using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Microsoft.JSInterop;
using Newtonsoft.Json.Linq;
using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Accountant;
using StoriesProject.Services.ApiUrldefinition;
using System.Security.Claims;

namespace StoriesProject.Services
{
    public interface IAccoutantsService: IBaseService
    {
        Task<ResponseOutput<string>> Login(LoginViewModel loginViewModel);
        Task<ResponseOutput<string>> Register(AccountantRegister loginViewModel);
    }
    public class AccoutantsService: BaseService, IAccoutantsService
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        

        public AccoutantsService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {
            
        }

        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> Login(LoginViewModel loginViewModel)
        {
            var url = AccountantApiUrlDef.Login();
            var res = await RequestFullPostAsync<string>(url, loginViewModel);
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

        
    }
}
