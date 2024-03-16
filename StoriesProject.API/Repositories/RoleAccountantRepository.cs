using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface IRoleAccountantRepository : IBaseRepository<RoleAccountant>
    {
    }

    public class RoleAccountantRepository : BaseRepository<RoleAccountant>, IRoleAccountantRepository
    {
        public RoleAccountantRepository(StoriesContext context) : base(context)
        {
        }
    }
}
