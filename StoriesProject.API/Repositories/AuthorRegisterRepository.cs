using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{

    public interface IAuthorRegisterRepository : IBaseRepository<AuthorRegister>
    {
    }

    public class AuthorRegisterRepository : BaseRepository<AuthorRegister>, IAuthorRegisterRepository
    {
        public AuthorRegisterRepository(IUnitOfWork entities) : base(entities)
        {
        }
    }
}
