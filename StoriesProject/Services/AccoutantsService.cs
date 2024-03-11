using Azure.Core;
using Microsoft.AspNetCore.Identity;
using Microsoft.JSInterop;
using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Accountant;
using StoriesProject.Pages.UserPages;
using StoriesProject.Services.ApiUrldefinition;

namespace StoriesProject.Services
{
    public interface IAccoutantsService : IBaseService
    {
        Task<ResponseOutput<LoginResponse>> Login(LoginViewModel loginViewModel);
        Task<ResponseOutput<string>> Register(AccountantRegister loginViewModel);
        Task<ResponseOutput<string>> RegisterAuthorAccountant(AuthorRegisterModel register);
        Task<List<AuthorRegister>> GetRegisterAccountantsByRole();
        Task<List<Accountant>> GetAllAccountants();
        Task<ResponseOutput<string>> ApprovedAccountant(Guid regiserId);
        Task<ResponseOutput<string>> DeniedAccountant(Guid regiserId);
        Task<ResponseOutput<string>> UpdateLockedAccountant(LockedAccountantParam param);
        Task<AccountantUpdate> GetUserInforGeneric();
        Task<ResponseOutput<string>> UpdateUserInfor(AccountantUpdate account);
        Task<ResponseOutput<string>> ChangePassword(ChangPasswordVM password);
    }
        
    public class AccoutantsService : BaseService, IAccoutantsService
    {
        public AccoutantsService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {

        }

        public async Task<ResponseOutput<string>> ApprovedAccountant(Guid regiserId)
        {
            var url = AccountantApiUrlDef.ApprovedAccountant();
            return await RequestFullAuthenPostAsync<string>(url, regiserId);
        }

        public async Task<ResponseOutput<string>> DeniedAccountant(Guid regiserId)
        {
            var url = AccountantApiUrlDef.DeniedAccountant();
            return await RequestFullAuthenPostAsync<string>(url, regiserId);
        }

        public async Task<List<AuthorRegister>> GetRegisterAccountantsByRole()
        {
            var url = AccountantApiUrlDef.GetRegisterAccountantsByRole();
            return await RequestAuthenGetAsync<List<AuthorRegister>>(url);
        }

        public async Task<List<Accountant>> GetAllAccountants()
        {
            var url = AccountantApiUrlDef.GetAllAccountants();
            return await RequestAuthenGetAsync<List<Accountant>>(url);
        }

        public async Task<ResponseOutput<string>> UpdateLockedAccountant(LockedAccountantParam param)
        {
            var url = AccountantApiUrlDef.UpdateLockedAccountant();
            return await RequestFullAuthenPostAsync<string>(url, param);
        }
        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<LoginResponse>> Login(LoginViewModel loginViewModel)
        {
            var url = AccountantApiUrlDef.Login();
            var res = await RequestFullPostAsync<LoginResponse>(url, loginViewModel);
            return res;
        }

        /// <summary>
        /// Hàm xử lý đăng ký
        /// CreatedBy ntthe 04.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> Register(AccountantRegister loginViewModel)
        {
            var url = AccountantApiUrlDef.Register();
            return await RequestFullPostAsync<string>(url, loginViewModel);
        }
        /// <summary>
        /// Đăng ký tác giả
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> RegisterAuthorAccountant(AuthorRegisterModel register)
        {
            var url = AccountantApiUrlDef.RegisterAuthorAccountant();
            return await RequestFullAuthenPostAsync<string>(url, register) ;
        }

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        /// <param name="loginViewModel"></param>
        /// <returns></returns>
        public async Task<AccountantUpdate> GetUserInforGeneric()
        {
            var url = AccountantApiUrlDef.GetUserInforGeneric();
            return await RequestAuthenGetAsync<AccountantUpdate>(url);
        }

        /// <summary>
        /// Cập nhật thông tin user
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> UpdateUserInfor(AccountantUpdate account)
        {
            var url = AccountantApiUrlDef.UpdateUserInfor();
            return await RequestFullAuthenPostAsync<string>(url, account);
        }

        /// <summary>
        /// Cập nhật thông tin user
        /// </summary>
        /// <param name="account"></param>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> ChangePassword(ChangPasswordVM password)
        {
            var url = AccountantApiUrlDef.ChangePassword();
            return await RequestFullAuthenPostAsync<string>(url, password);
        }
    }
}
