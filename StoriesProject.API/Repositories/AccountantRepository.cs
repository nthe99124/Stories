using StoriesProject.API.Common.Repository;
using StoriesProject.API.Common.Ulti;
using StoriesProject.API.Model.BaseEntity;
using StoriesProject.API.Repositories.Base;

namespace StoriesProject.API.Repositories
{
    public interface IAccountantsRepository: IBaseRepository<Accountant>
    {
        Task<Accountant?> GetUserByUserNameAndPass(string userName, string password);
    }
    public class AccountantRepository: BaseRepository<Accountant>, IAccountantsRepository
    {
        public AccountantRepository(IUnitOfWork entities):base(entities)
        {
            
        }

        public async Task<Accountant?> GetUserByUserNameAndPass(string userName, string password)
        {
            var passwordEncode = HashCodeUlti.EncodePassword(password);
            var user = await FindBy(a => a.UserName == userName && a.Password == passwordEncode);
            return user.FirstOrDefault();
        }
    }
}
