using Microsoft.JSInterop;
using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Services.ApiUrldefinition;

namespace StoriesProject.Services
{
    public interface IStoriesService : IBaseService
    {
        Task<List<StoryGeneric>> GetTop10NewStory();
        Task<List<StoryGeneric>> GetTop10HotStory();
        Task<List<StoryGeneric>> GetTop10FreeStory();
        Task<List<StoryGeneric>> GetTop10PaidStory();
        Task<List<StoryGeneric>> GetTop10NewVervionStory();
        Task<List<StoryGeneric>> GetFavoriteStory();
        Task<List<StoryGeneric>> GetAllStoryByTopic(Guid topicId);
        Task<List<StoryGeneric>> GetNewVervionStoryByDay(DateTime dateTime);
    }
    public class StoriesService : BaseService, IStoriesService
    {
        public StoriesService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js) : base(cache, httpClientFactory, config, js)
        {
            
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetTop10NewStory()
        {
            var url = StoriesApiUrlDef.GetTop10NewStory();
            return await RequestGetAsync<List<StoryGeneric>>(url);
        }


        /// <summary>
        /// Hàm xử lý lấy 10 truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetTop10HotStory()
        {
            var url = StoriesApiUrlDef.GetTop10HotStory();
            return await RequestGetAsync<List<StoryGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện miễn phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetTop10FreeStory()
        {
            var url = StoriesApiUrlDef.GetTop10FreeStory();
            return await RequestGetAsync<List<StoryGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện trả phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetTop10PaidStory()
        {
            var url = StoriesApiUrlDef.GetTop10PaidStory();
            return await RequestGetAsync<List<StoryGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetTop10NewVervionStory()
        {
            var url = StoriesApiUrlDef.GetTop10NewVervionStory();
            return await RequestGetAsync<List<StoryGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện yêu thích
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetFavoriteStory()
        {
            var url = StoriesApiUrlDef.GetFavoriteStory();
            return await RequestAuthenGetAsync<List<StoryGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện theo chủ đề
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetAllStoryByTopic(Guid topicId)
        {
            var url = StoriesApiUrlDef.GetAllStoryByTopic(topicId);
            return await RequestGetAsync<List<StoryGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryGeneric>> GetNewVervionStoryByDay(DateTime dateTime)
        {
            var url = StoriesApiUrlDef.GetNewVervionStoryByDay(dateTime);
            return await RequestAuthenGetAsync<List<StoryGeneric>>(url);
        }

        
    }
}
