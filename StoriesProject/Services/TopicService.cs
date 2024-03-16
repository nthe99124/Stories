using Microsoft.JSInterop;
using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Services.ApiUrldefinition;

namespace StoriesProject.Services
{
    public interface ITopicService : IBaseService
    {
        Task<List<Topic>> GetAllTopic();
        Task<List<TopicGeneric>> GetAllTopicSortByStory();
    }
    public class TopicService : BaseService, ITopicService
    {
        public TopicService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {
            
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<Topic>> GetAllTopic()
        {
            var url = TopicApiUrlDef.GetAllTopic();
            return await RequestGetAsync<List<Topic>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề sort theo số lượng truyện
        /// CreatedBy ntthe 16.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<TopicGeneric>> GetAllTopicSortByStory()
        {
            var url = TopicApiUrlDef.GetAllTopicSortByStory();
            return await RequestGetAsync<List<TopicGeneric>>(url);
        }
    }
}
