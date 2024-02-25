using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using StoriesProject.Common.Cache;
using StoriesProject.Common.Constant;
using StoriesProject.Common.Ulti;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Models.DTO.Accountant;
using StoriesProject.Repositories;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using WebCrawlTMDT.Services.Base;

namespace StoriesProject.Services
{
    public interface IAccoutantsService
    {
        Task<AccountGenericDTO?> Login(string userName, string password);
        Task<AccountGenericDTO?> Register(Accountant account);
        Task Logout();
    }
    public class AccoutantsService: BaseService, IAccoutantsService
    {
        private readonly IAccountantsRepository _accoutantsRepository;
        public AccoutantsService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IAccountantsRepository accoutantsRepository) : base(httpContextAccessor, cache)
        {
            _accoutantsRepository = accoutantsRepository;
        }

        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="ipAddress"></param>
        /// <returns></returns>
        public async Task<AccountGenericDTO?> Login(string userName, string password)
        {
            var account = await _accoutantsRepository.GetUserByEmailAndPass(userName, password);
            if (account != null)
            {
                var authenObject = await HandleSignIn(account);

                return authenObject;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Hàm xử lý đăng kí
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="account"></param>
        /// <param name="hasLogin"></param>
        /// <returns></returns>
        public async Task<AccountGenericDTO?> Register(Accountant account)
        {
            var acc = await _accoutantsRepository.GetUserByEmailAndPass(account.UserName, account.Password);
            if (acc == null)
            {
                // encode pass trước khi cất
                account.Password = HashCodeUlti.EncodePassword(account.Password);
                // cất
                await _accoutantsRepository.Create(account);

                // sign thông tin vào để login
                var authenObject = await HandleSignIn(account);
                return authenObject;
            }
            else
            {
                return null;
            }
        }

        /// <summary>
        /// Hàm xử lý đăng xuất
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task Logout()
        {
            // SignOut
            await _httpContextAccessor.HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            _httpContextAccessor.HttpContext.Session.Clear();
        }

        #region Private Method

        /// <summary>
        /// Hàm ghi thông tin vào claim
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        private AccountAuthenDTO SignClaimWriteToken(Accountant account)
        {
            var listClaim = new List<Claim>()
                {
                    new Claim(JwtRegisteredClaimNames.Sid, Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Sub, account.UserName),
                    new Claim(JwtRegisteredClaimsNamesConstant.Coin, account.Coin.ToString()),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
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

        private async Task<AccountGenericDTO> HandleSignIn(Accountant account)
        {
            var authenObject = SignClaimWriteToken(account);
            if (authenObject != null)
            {
                await _httpContextAccessor.HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, 
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
        #endregion
    }
}
