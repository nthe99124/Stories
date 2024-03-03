using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Services.ApiUrldefinition;

namespace StoriesProject.Services
{
    public interface IStoriesService : IBaseService
    {
        Task<List<Story>> GetTop10NewStory();
        Task<List<Story>> GetTop10HotStory();
        Task<List<Story>> GetTop10FreeStory();
        Task<List<Story>> GetTop10PaidStory();
        Task<List<Story>> GetTop10NewVervionStory();
    }
    public class StoriesService : BaseService, IStoriesService
    {
        public StoriesService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config) : base(cache, httpClientFactory, config)
        {
            
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<Story>> GetTop10NewStory()
        {
            var url = StoriesApiUrlDef.GetTop10NewStory();
            return await RequestGetAsync<List<Story>>(url);
        }


        /// <summary>
        /// Hàm xử lý lấy 10 truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<Story>> GetTop10HotStory()
        {
            var url = StoriesApiUrlDef.GetTop10HotStory();
            return await RequestGetAsync<List<Story>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện miễn phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<Story>> GetTop10FreeStory()
        {
            var url = StoriesApiUrlDef.GetTop10FreeStory();
            return await RequestGetAsync<List<Story>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện trả phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<Story>> GetTop10PaidStory()
        {
            var url = StoriesApiUrlDef.GetTop10PaidStory();
            return await RequestGetAsync<List<Story>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<Story>> GetTop10NewVervionStory()
        {
            var url = StoriesApiUrlDef.GetTop10NewVervionStory();
            return await RequestGetAsync<List<Story>>(url);
        }
    }
}
