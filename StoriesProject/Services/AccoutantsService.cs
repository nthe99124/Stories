using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.ViewModel.Accountant;
using StoriesProject.Services.ApiUrldefinition;

namespace StoriesProject.Services
{
    public interface IAccoutantsService: IBaseService
    {
        Task<string> Login(LoginViewModel loginViewModel);
    }
    public class AccoutantsService: BaseService, IAccoutantsService
    {
        public AccoutantsService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config) : base(cache, httpClientFactory, config)
        {
            
        }

        /// <summary>
        /// Hàm xử lý login
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<string> Login(LoginViewModel loginViewModel)
        {
            var url = AccountantApiUrlDef.Login();
            return await RequestPostAsync<string>(url, loginViewModel);
        }
    }
}
