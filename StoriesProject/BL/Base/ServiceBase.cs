using StoriesProject.Common.Cache;
using StoriesProject.Common.Repository;
using StoriesProject.Model.DTO;
using System.IdentityModel.Tokens.Jwt;

namespace WebCrawlTMDT.BL
{
    public class ServiceBase
    {
        protected static IHttpContextAccessor _httpContextAccessor;
        protected IDistributedCacheCustom _cache;
        public ServiceBase(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache)
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
