using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface IRoleRepository : IBaseRepository<Role>
    {
    }

    public class RoleRepository : BaseRepository<Role>, IRoleRepository
    {
        public RoleRepository(StoriesContext context) : base(context)
        {
        }
    }
}
