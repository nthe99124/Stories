using AutoMapper;
using StoriesProject.API.Common.Cache;
using StoriesProject.API.Common.Constant;
using StoriesProject.API.Common.Repository;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO.Accountant;
using System.Net.WebSockets;
using System.Security.Claims;
using System.Text.Json;

namespace StoriesProject.API.Services.Base
{
    public class BaseService
    {
        protected IHttpContextAccessor _httpContextAccessor;
        protected IDistributedCacheCustom _cache;
        protected IUnitOfWork _unitOfWork;
        protected IMapper _mapper;
        public BaseService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            _cache = cache;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public AccountGenericDTO? GetUserAuthen()
        {
            var userInfor = new AccountGenericDTO();
            var userContext = _httpContextAccessor?.HttpContext?.User;
            if (userContext != null && userContext.Identity != null && userContext.Identity.IsAuthenticated)
            {
                userInfor.UserName = userContext?.FindFirst(JwtRegisteredClaimsNamesConstant.Sub)?.Value;
                userInfor.Coin = long.Parse(userContext?.FindFirst(JwtRegisteredClaimsNamesConstant.Coin)?.Value);
                userInfor.AccoutantId = Guid.Parse(userContext?.FindFirst(JwtRegisteredClaimsNamesConstant.AccId)?.Value);
                userInfor.RoleList = userContext.FindAll(JwtRegisteredClaimsNamesConstant.Role).Select(c => c.Value).ToList();
            }
            else
            {
                return null;
            }

            return userInfor;
        }

        public Dictionary<string, Role>? GetRoleAuthen()
        {
            var roleInfor = new Dictionary<string,Role>();
            var userContext = _httpContextAccessor?.HttpContext?.User;
            if (userContext != null && userContext.Identity != null && userContext.Identity.IsAuthenticated)
            {
                var lstRoleJson = userContext.FindAll(JwtRegisteredClaimsNamesConstant.RoleInfor).Select(c => c.Value).ToList();
                foreach (var item in lstRoleJson)
                {
                    if (!string.IsNullOrEmpty(item))
                    {
                        var roleObject = JsonSerializer.Deserialize<Role>(item);
                        roleInfor.Add(roleObject.RoleName, roleObject);
                    }
                }
            }
            else
            {
                return null;
            }

            return roleInfor;
        }

        /// <summary>
        /// TODO: Hàm này đẻ ra chỉ để lấy role có grant type lớn nhất, sau này thống nhất lại thì phải làm động không làm thế này
        /// </summary>
        /// <returns></returns>
        public Role? GetMaxRoleAuthen()
        {
            var roleList = GetRoleAuthen();
            if (roleList.ContainsKey(RoleConstant.Admin))
            {
                // là admin lấy luôn role admin luôn
                return roleList[RoleConstant.Admin];
            }
            else if (roleList.ContainsKey(RoleConstant.Employee))
            {
                return roleList[RoleConstant.Employee];
            }
            else if (roleList.ContainsKey(RoleConstant.Author))
            {
                return roleList[RoleConstant.Author];
            }
            else if (roleList.ContainsKey(RoleConstant.Customer))
            {
                return roleList[RoleConstant.Customer];
            }
            return null;
        }
    }
}
