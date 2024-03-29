﻿using Microsoft.Data.SqlClient;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Common.Ulti;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface IAccountantsRepository: IBaseRepository<Accountant>
    {
        Task<Accountant?> GetUserByUserNameAndPass(string userName, string password);

        Task<IEnumerable<Role>?> GetListRoleByAccId(Guid accId);
    }
    public class AccountantsRepository : BaseRepository<Accountant>, IAccountantsRepository
    {
        public AccountantsRepository(StoriesContext context):base(context)
        {
            
        }

        public async Task<Accountant?> GetUserByUserNameAndPass(string userName, string password)
        {
            var passwordEncode = HashCodeUlti.EncodePassword(password);
            var user = await FindBy(a => a.UserName == userName && a.Password == passwordEncode);
            return user.FirstOrDefault();
        }

        public async Task<IEnumerable<Role>?> GetListRoleByAccId(Guid accId)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@AccID", accId)
            };
            var roleList = ExecuteStoredProcedureObject<Role>("GetRoleByAccId", param);
            return roleList;
        }
    }
}
