using AutoMapper;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using StoriesProject.API.Common.Cache;
using StoriesProject.API.Common.Constant;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Common.Ulti;
using StoriesProject.API.Services.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO.Accountant;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Accountant;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Text.Json;

namespace StoriesProject.API.Services
{
    public interface IAccountantsService
    {
        Task<LoginResponse?> Login(string userName, string password);
        Task<bool> Register(AccountantRegister account);
        Task<bool> RegisterAuthorAccountant(AuthorRegisterModel account);
        void Logout();
        AccountGenericDTO? GetUserInfor();
        Task<RestOutput> UpdateUserInfor(AccountantUpdate account);
        Task<RestOutput> ChangePassword(string newPassword, string oldPassword);
        Task<IEnumerable<AuthorRegister>?> GetRegisterAccountantsByRole();
        Task<IEnumerable<Accountant>?> GetAll();
        Task<bool?> ApprovedAccountant(Guid registerId);
        Task<bool?> DeniedAccountant(Guid registerId);
        Task<bool?> UpdateLockedAccountant(LockedAccountantParam param);
        Task<AccountantUpdate?> GetUserInforGeneric();
    }
    public class AccountantsService: BaseService, IAccountantsService
    {
        private readonly IConfiguration _configuration;
        public AccountantsService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IUnitOfWork unitOfWork, IConfiguration configuration, IMapper mapper) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {
            _configuration = configuration;
        }

        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<LoginResponse?> Login(string userName, string password)
        {
            var account = await _unitOfWork.AccountantsRepository.GetUserByUserNameAndPass(userName, password);
            if (account != null)
            {
                if(account.IsLocked)
                {
                    throw new Exception("Tài khoản đã bị khóa");
                }

                // lấy role
                var listRole = await _unitOfWork.AccountantsRepository.GetListRoleByAccId(account.Id);
                var token = HandleSignInAndGenerateToken(account, listRole);

                if (token != null)
                {
                    // ghi token vào cookie
                    _httpContextAccessor?.HttpContext?.Response.Cookies.Append("access_token", token, new CookieOptions
                    {
                        HttpOnly = true, // Set HttpOnly to true for security
                        Secure = true,   // Set Secure to true if your site uses HTTPS
                        SameSite = SameSiteMode.Strict, // Set SameSite to Strict for added security
                        Expires = DateTime.UtcNow.AddMinutes(15) // Set the expiration time
                    });
                    var result = new LoginResponse();
                    result.Token = token;
                    result.RoleList = listRole.Select(item => item.RoleName).ToList();
                    return result;
                }
            }
            else
            {
                throw new Exception("Sai tài khoản hoặc mật khẩu");
            }
            return null;
        }

        /// <summary>
        /// Hàm xử lý đăng kí
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="account"></param>
        /// <param name="hasLogin"></param>
        /// <returns></returns>
        public async Task<bool> Register(AccountantRegister account)
        {
            var acc = await _unitOfWork.AccountantsRepository.GetUserByUserNameAndPass(account.UserName, account.Password);
            if (acc == null)
            {
                // encode pass trước khi cất
                account.Password = HashCodeUlti.EncodePassword(account.Password);
                var accountInsert = _mapper.Map<Accountant>(account);
                // cất
                _unitOfWork.AccountantsRepository.Create(accountInsert);
                _unitOfWork.Commit();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Đăng ký tác giả
        /// </summary>
        public async Task<bool> RegisterAuthorAccountant(AuthorRegisterModel register)
        {
            // lưu dữ liệu vào bảng AuthorRegister
            try
            {
                var userId = GetUserAuthen()?.AccoutantId;
                if (register != null && userId != null)
                {
                    var registerMapping = _mapper.Map<AuthorRegister>(register);
                    registerMapping.AccountantId = userId.Value;
                    _unitOfWork.AuthorRegisterRepository.Create(registerMapping);
                    _unitOfWork.Commit();
                } else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Hàm xử lý đăng xuất
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <returns></returns>
        public void Logout()
        {
            // SignOut
            _httpContextAccessor?.HttpContext?.Response?.Cookies.Delete("access_token");
        }

        /// <summary>
        /// Hàm lấy thông tin user theo authen
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <returns></returns>
        public AccountGenericDTO? GetUserInfor()
        {
            return GetUserAuthen();
        }

        /// <summary>
        /// Hàm lấy thông tin user
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<AccountantUpdate?> GetUserInforGeneric()
        {
            var userName = GetUserAuthen()?.UserName;
            if (!string.IsNullOrEmpty(userName))
            {
                var userFull = await _unitOfWork.AccountantsRepository.FirstOrDefault(item => item.UserName == userName);
                if (userFull != null)
                {
                    return new AccountantUpdate
                    {
                        UserName = userFull.UserName,
                        Name = userFull.Name,
                        Email = userFull.Email,
                        Gender = userFull.Gender,
                        DateOfBirth = userFull.DateOfBirth,
                        Introduce = userFull.Introduce,
                        ImgAvatar = userFull.ImgAvatar
                    };
                }
            }
            return null;
        }

        /// <summary>
        /// Hàm xử lý cập nhật thông tin user
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="account"></param>
        /// <param name="hasLogin"></param>
        /// <returns></returns>
        public async Task<RestOutput> UpdateUserInfor(AccountantUpdate account)
        {
            var res = new RestOutput();
            if (account != null)
            {
                var isExistUser = await _unitOfWork.AccountantsRepository.CheckExitsByCondition(item => item.UserName == account.UserName);
                if (isExistUser)
                {
                    res.ErrorEventHandler("Username đã tồn tại");
                }
                else
                {
                    var userNameCurrent = GetUserAuthen()?.UserName;
                    var accUserUpdate = await _unitOfWork.AccountantsRepository.FirstOrDefault(item => item.UserName == userNameCurrent);
                    if (accUserUpdate != null)
                    {
                        // update từng field của user
                        accUserUpdate.UserName = account.UserName;
                        accUserUpdate.Name = account.Name;
                        accUserUpdate.Email = account.Email;
                        accUserUpdate.Gender = account.Gender;
                        accUserUpdate.DateOfBirth = account.DateOfBirth;
                        accUserUpdate.Introduce = account.Introduce;
                        accUserUpdate.ImgAvatar = account.ImgAvatar;

                        // lưu acc
                        _unitOfWork.Commit();
                        var dataResponse = new UserInforGeneric
                        {
                            UserName = accUserUpdate.UserName,
                            ImgAvatar = accUserUpdate.ImgAvatar,
                            Coin = accUserUpdate.Coin,
                        };
                        res.SuccessEventHandler(dataResponse);
                    }
                }
            }
            
            return res;
            
        }

        /// <summary>
        /// Hàm xử lý cập nhật password
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="account"></param>
        /// <param name="hasLogin"></param>
        /// <returns></returns>
        public async Task<RestOutput> ChangePassword(string newPassword, string oldPassword)
        {
            var res = new RestOutput();
            if (!string.IsNullOrEmpty(newPassword) && !string.IsNullOrEmpty(oldPassword))
            {
                // nếu trùng là chửi
                if (newPassword.Trim() == oldPassword.Trim())
                {
                    res.ErrorEventHandler("Password mới trùng với password cũ");
                }
                else
                {
                    var userNameCurrent = GetUserAuthen()?.UserName;
                    var accUserUpdate = await _unitOfWork.AccountantsRepository.FirstOrDefault(item => item.UserName == userNameCurrent);

                    var oldPasswordHash = HashCodeUlti.EncodePassword(oldPassword);
                    
                    if (accUserUpdate != null && accUserUpdate.Password != oldPasswordHash)
                    {
                        res.ErrorEventHandler("Password cũ không chính xác");
                    }
                    else
                    {
                        var passWordHash = HashCodeUlti.EncodePassword(newPassword);
                        // update từng field của user
                        accUserUpdate.Password = passWordHash;

                        // lưu acc
                        _unitOfWork.Commit();
                        res.SuccessEventHandler(true);
                    }
                }
            }
            else
            {
                res.ErrorEventHandler("Password không được để trống");
            }
            
            return res;
        }

        /// <summary>
        /// Lấy danh sách user
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<AuthorRegister>?> GetRegisterAccountantsByRole()
        {
            var roleMax = GetMaxRoleAuthen();
            if (roleMax != null)
            {
                var result = await _unitOfWork.AuthorRegisterRepository.GetAll();
                return result;
            }
            return null;
        }

        /// <summary>
        /// Lấy toàn bộ danh sách user
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Accountant>?> GetAll()
        {
            var result = await _unitOfWork.AccountantsRepository.GetAll();

            return result;
        }

        /// <summary>
        /// Cấp quyền người dùng
        /// </summary>
        /// <param name="accountantId"></param>
        /// <returns></returns>
        public async Task<bool?> ApprovedAccountant(Guid regiserId)
        {
            // Lấy thông tin đăng ký
            var registerData = await _unitOfWork.AuthorRegisterRepository.FirstOrDefault(f => f.AuthorRegisterId == regiserId);

            if(registerData == null) 
            {
                return false;
            }

            // Lấy role của tác giả
            // tạm fix cứng quyền tác giả
            var authorRole = await _unitOfWork.RoleRepository.FirstOrDefault(f => f.RoleName == "Author");

            if(authorRole != null)
            {
                try
                {
                    // insert dữ liệu vào roleaccountant
                    var userRole = new RoleAccountant()
                    {
                        AccountId = registerData.AccountantId,
                        RoleId = authorRole.Id
                    };
                    _unitOfWork.RoleAccountantRepository.Create(userRole);
                    // xóa dữ liệu đăng ký
                    _unitOfWork.AuthorRegisterRepository.Delete(registerData);
                }
                catch (Exception ex)
                {
                    return false;
                }

            }
            return true;
        }
        /// <summary>
        /// Từ chối phê duyệt quyền tác giả
        /// </summary>
        /// <param name="regiserId"></param>
        /// <returns></returns>
        public async Task<bool?> DeniedAccountant(Guid regiserId)
        {
            // Lấy thông tin đăng ký
            var registerData = await _unitOfWork.AuthorRegisterRepository.FirstOrDefault(f => f.AuthorRegisterId == regiserId);

            if (registerData == null)
            {
                return false;
            }

            try
            {
                // xóa dữ liệu đăng ký
                _unitOfWork.AuthorRegisterRepository.Delete(registerData);
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Cập nhật trạng thái khóa tài khoản
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<bool?> UpdateLockedAccountant(LockedAccountantParam param)
        {
            var user = await _unitOfWork.AccountantsRepository.FirstOrDefault(f => f.Id == param.AccountantId);

            if (user == null)
            {
                return false;
            }

            try
            {
                user.IsLocked = param.IsLocked;
                _unitOfWork.Commit();
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }

        #region Private Method

        #region phần đăng nhập sử dụng BearToken
        /// <summary>
        /// Hàm ghi claim và gen AccessToken
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private string? HandleSignInAndGenerateToken(Accountant account, IEnumerable<Role> listRole)
        {
            if (_configuration != null)
            {
                var jwtToken = new JwtSecurityTokenHandler();
                var secretKeyByte = Encoding.UTF8.GetBytes(_configuration.GetSection("Jwt").GetSection("SecretKey").Value);
                var tokenDescription = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                        new Claim(JwtRegisteredClaimsNamesConstant.Sid, Guid.NewGuid().ToString()),
                        new Claim(JwtRegisteredClaimsNamesConstant.Sub, account.UserName),
                        new Claim(JwtRegisteredClaimsNamesConstant.Coin, account.Coin.ToString()),
                        new Claim(JwtRegisteredClaimsNamesConstant.AccId, account.Id.ToString()),
                        new Claim(JwtRegisteredClaimsNamesConstant.Jti, Guid.NewGuid().ToString()),
                    }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha256)
                };

                // xử lý add Role

                if (listRole != null && listRole.Count() > 0)
                {
                    foreach (var role in listRole)
                    {
                        if (role != null && !string.IsNullOrEmpty(role.RoleName))
                        {
                            tokenDescription?.Subject.AddClaim(new Claim(JwtRegisteredClaimsNamesConstant.Role, role.RoleName));
                            tokenDescription?.Subject.AddClaim(new Claim(JwtRegisteredClaimsNamesConstant.RoleInfor, JsonSerializer.Serialize(role)));
                            
                        }
                    }
                }

                var token = jwtToken.CreateToken(tokenDescription);
                return jwtToken.WriteToken(token);
            }
            else
            {
                return null;
            }
        }
        #endregion

        #region Luồng đăng nhập với cookie
        /// <summary>
        /// Hàm xử lý sign cho đăng nhập với loại đăng nhập là cookie
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Obsolete]
        private async Task<AccountGenericDTO?> HandleSignInWithCookieSign(Accountant account)
        {
            var authenObject = SignClaimWriteToken(account);
            if (authenObject != null)
            {
                await _httpContextAccessor?.HttpContext?.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
                                                            new ClaimsPrincipal(authenObject.ClaimsIdentity), 
                                                            authenObject.AuthProperties);
                var data = new AccountGenericDTO
                {
                    UserName = account.UserName,
                    Coin = account.Coin,
                };
                return data;
            }

            return null;
        }

        /// <summary>
        /// Hàm ghi thông tin vào claim
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        [Obsolete]
        private AccountAuthenDTO SignClaimWriteToken(Accountant account)
        {
            var listClaim = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimsNamesConstant.Sid, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimsNamesConstant.Sub, account.UserName),
                    new Claim(JwtRegisteredClaimsNamesConstant.Coin, account.Coin.ToString()),
                    new Claim(JwtRegisteredClaimsNamesConstant.Jti, Guid.NewGuid().ToString())
                };

            //if (account.IsAdmin)
            //{
            //    listClaim.Add(new Claim(ClaimTypes.Role, RoleConstant.Admin));
            //}
            //else
            //{
            //    listClaim.Add(new Claim(ClaimTypes.Role, RoleConstant.Customer));
            //}

            var claimsIdentity = new ClaimsIdentity(listClaim, CookieAuthenticationDefaults.AuthenticationScheme);
            var authProperties = new AuthenticationProperties
            {
                // Thông tin cấu hình cookie, ví dụ như thời gian hết hạn
                ExpiresUtc = DateTimeOffset.UtcNow.AddDays(1),
                IsPersistent = false, // Đặt true nếu bạn muốn cookie "nhớ đăng nhập" qua các session
                AllowRefresh = false
            };

            return new AccountAuthenDTO
            {
                ClaimsIdentity = claimsIdentity,
                AuthProperties = authProperties,
                UserName = account.UserName,
                Coin = account.Coin
            };
        
        }

        /// <summary>
        /// Hàm xử lý đăng xuất
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task LogoutCookie()
        {
            // SignOut
            await _httpContextAccessor?.HttpContext?.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor?.HttpContext?.Session.Clear();
        }

        
        #endregion
        #endregion
    }
}
