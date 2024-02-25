using StoriesProject.Common.Repository;
using StoriesProject.Common.Ulti;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Repositories.Base;

namespace StoriesProject.Repositories
{
    public interface IAccountantsRepository: IBaseRepository<Accountant>
    {
        Task<Accountant?> GetUserByEmailAndPass(string userName, string password);
    }
    public class AccountantRepository: BaseRepository<Accountant>, IAccountantsRepository
    {
        public AccountantRepository(IUnitOfWork entities):base(entities)
        {
            
        }

        public async Task<Accountant?> GetUserByEmailAndPass(string userName, string password)
        {
            var passwordEncode = HashCodeUlti.EncodePassword(password);
            var user = await FindBy(a => a.UserName == userName && a.Password == passwordEncode);
            return user.FirstOrDefault();
        }
    }
}
