using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;

namespace StoriesProject.Services
{
    public interface IAccoutantsService: IBaseService
    {

    }
    public class AccoutantsService: BaseService, IAccoutantsService
    {
        public AccoutantsService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config) : base(cache, httpClientFactory, config)
        {
            
        }
    }
}
