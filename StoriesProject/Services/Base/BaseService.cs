using StoriesProject.Common.Cache;
using StoriesProject.Models.DTO.Accountant;
using System.IdentityModel.Tokens.Jwt;

namespace WebCrawlTMDT.Services.Base
{
    public class BaseService
    {
        protected static IHttpContextAccessor _httpContextAccessor;
        protected IDistributedCacheCustom _cache;
        public BaseService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public static AccountGenericDTO GetUserAuthen()
        {
            var userInfor = new AccountGenericDTO();
            if (_httpContextAccessor.HttpContext.User != null && _httpContextAccessor.HttpContext.User.Identity.IsAuthenticated)
            {
                var accUser = _httpContextAccessor.HttpContext.User;
                userInfor.UserName = accUser.FindFirst(JwtRegisteredClaimNames.Sub)?.Value;
            }
            else
            {
                return null;
            }
            return userInfor;
        }
    }
}
