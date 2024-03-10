using Microsoft.Data.SqlClient;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Common.Ulti;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO.Accountant;

namespace StoriesProject.API.Repositories
{
    public interface IAccountantsRepository: IBaseRepository<Accountant>
    {
        Task<Accountant?> GetUserByUserNameAndPass(string userName, string password);

        Task<IEnumerable<AuthorRegister>?> GetRegisterAccountantsByRole(Guid roleID);
    }
    public class AccountantRepository : BaseRepository<Accountant>, IAccountantsRepository
    {
        public AccountantRepository(IUnitOfWork entities):base(entities)
        {
            
        }
        // TODO: tạm thời lấy hết danh sách user ở bảng Accountants, sau lấy ở bảng đăng ký
        public async Task<IEnumerable<AuthorRegister>?> GetRegisterAccountantsByRole(Guid roleID)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@RoleID", roleID)
            };
            var lstAccountants = _entities.ExecuteStoredProcedureObject<AuthorRegister>("GetRegisterAccountantsByRole", param);
            return lstAccountants;
        }

        public async Task<Accountant?> GetUserByUserNameAndPass(string userName, string password)
        {
            var passwordEncode = HashCodeUlti.EncodePassword(password);
            var user = await FindBy(a => a.UserName == userName && a.Password == passwordEncode);
            return user.FirstOrDefault();
        }
    }
}
