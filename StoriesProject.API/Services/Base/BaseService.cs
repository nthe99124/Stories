using StoriesProject.API.Common.Cache;
using StoriesProject.API.Common.Constant;
using StoriesProject.API.Models.DTO.Accountant;

namespace StoriesProject.API.Services.Base
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
                userInfor.UserName = accUser.FindFirst(JwtRegisteredClaimsNamesConstant.Sub)?.Value;
            }
            else
            {
                return null;
            }
            return userInfor;
        }
    }
}
