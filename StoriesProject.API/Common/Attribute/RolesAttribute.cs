using Microsoft.AspNetCore.Authorization;

namespace StoriesProject.API.Common.Attribute
{
    public class RolesAttribute : AuthorizeAttribute
    {
        public RolesAttribute(params string[] roles)
        {
            Roles = String.Join(",", roles);
        }
    }
}
