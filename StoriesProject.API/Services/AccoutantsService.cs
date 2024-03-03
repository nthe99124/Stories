using AutoMapper;
using Azure;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using StoriesProject.API.Common.Cache;
using StoriesProject.API.Common.Constant;
using StoriesProject.API.Common.Ulti;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO.Accountant;
using StoriesProject.Model.ViewModel.Accountant;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace StoriesProject.API.Services
{
    public interface IAccoutantsService
    {
        Task<string?> Login(string userName, string password);
        Task<bool> Register(AccountantRegister account);
        void Logout();
    }
    public class AccoutantsService: BaseService, IAccoutantsService
    {
        private readonly IAccountantsRepository _accoutantsRepository;
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;
        public AccoutantsService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IAccountantsRepository accoutantsRepository, IConfiguration configuration, IMapper mapper) : base(httpContextAccessor, cache)
        {
            _accoutantsRepository = accoutantsRepository;
            _configuration = configuration;
            _mapper = mapper;
        }

        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<string?> Login(string userName, string password)
        {
            var account = await _accoutantsRepository.GetUserByUserNameAndPass(userName, password);
            if (account != null)
            {
                var token = HandleSignInAndGenerateToken(account);

                if (token != null)
                {
                    // ghi token vào cookie
                    _httpContextAccessor?.HttpContext?.Response.Cookies.Append("access_token", token, new CookieOptions
                    {
                        HttpOnly = true, // Set HttpOnly to true for security
                        Secure = true,   // Set Secure to true if your site uses HTTPS
                        SameSite = SameSiteMode.Strict, // Set SameSite to Strict for added security
                        Expires = DateTimeOffset.UtcNow.AddMinutes(15) // Set the expiration time
                    });

                    return token;
                }
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
            var acc = await _accoutantsRepository.GetUserByUserNameAndPass(account.UserName, account.Password);
            if (acc == null)
            {
                // encode pass trước khi cất
                account.Password = HashCodeUlti.EncodePassword(account.Password);
                var accountInsert = _mapper.Map<Accountant>(account);
                // cất
                await _accoutantsRepository.Create(accountInsert);

                return true;
            }
            else
            {
                return false;
            }
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

        #region Private Method

        #region phần đăng nhập sử dụng BearToken
        /// <summary>
        /// Hàm ghi claim và gen AccessToken
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private string? HandleSignInAndGenerateToken(Accountant account)
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
                    new Claim(JwtRegisteredClaimsNamesConstant.Jti, Guid.NewGuid().ToString())
                }),
                    Expires = DateTime.UtcNow.AddMinutes(10),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(secretKeyByte), SecurityAlgorithms.HmacSha256)
                };
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
