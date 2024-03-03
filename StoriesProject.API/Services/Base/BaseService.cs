using StoriesProject.API.Common.Cache;
using StoriesProject.API.Common.Constant;
using StoriesProject.Model.DTO.Accountant;

namespace StoriesProject.API.Services.Base
{
    public class BaseService
    {
        protected IHttpContextAccessor _httpContextAccessor;
        protected IDistributedCacheCustom _cache;
        public BaseService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
        }

        public AccountGenericDTO? GetUserAuthen()
        {
            var userInfor = new AccountGenericDTO();
            var userContext = _httpContextAccessor?.HttpContext?.User;
            if (userContext != null && userContext.Identity != null && userContext.Identity.IsAuthenticated)
            {
                userInfor.UserName = userContext?.FindFirst(JwtRegisteredClaimsNamesConstant.Sub)?.Value;
            }
            else
            {
                return null;
            }

            return userInfor;
        }
    }
}
