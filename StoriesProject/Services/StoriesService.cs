using Microsoft.JSInterop;
using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO.Story;
using StoriesProject.Model.ViewModel;
using StoriesProject.Services.ApiUrldefinition;

namespace StoriesProject.Services
{
    public interface IStoriesService : IBaseService
    {
        Task<List<StoryAccountGeneric>> GetTop10NewStory();
        Task<List<StoryGeneric>> GetTop10HotStory();
        Task<List<StoryAccountGeneric>> GetTop10FreeStory();
        Task<List<StoryAccountGeneric>> GetTop10PaidStory();
        Task<List<StoryAccountGeneric>> GetTop10NewVersionStory();
        Task<List<StoryAccountGeneric>> GetFavoriteStory();
        Task<List<StoryAccountGeneric>> GetHistoryStoryRead();
        Task<List<StoryAccountGeneric>> GetAllStoryByTopic(Guid topicId);
        Task<List<StoryAccountGeneric>> GetNewVersionStoryByDay(DateTime dateTime);
        Task<StoryDetailFullDTO> GetStoryById(Guid? id);
        Task<List<StoryAccountGeneric>> GetStoryByCurrentAuthor();
        Task<ResponseOutput<string>> CreateStoryByAuthor(StoryRegisterVM storyRegister);
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
        public async Task<List<StoryAccountGeneric>> GetTop10NewStory()
        {
            var url = StoriesApiUrlDef.GetTop10NewStory();
            return await RequestGetAsync<List<StoryAccountGeneric>>(url);
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
        public async Task<List<StoryAccountGeneric>> GetTop10FreeStory()
        {
            var url = StoriesApiUrlDef.GetTop10FreeStory();
            return await RequestGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện trả phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>> GetTop10PaidStory()
        {
            var url = StoriesApiUrlDef.GetTop10PaidStory();
            return await RequestGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>> GetTop10NewVersionStory()
        {
            var url = StoriesApiUrlDef.GetTop10NewVervionStory();
            return await RequestGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện yêu thích
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>> GetFavoriteStory()
        {
            var url = StoriesApiUrlDef.GetFavoriteStory();
            return await RequestAuthenGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện đã đọc
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>> GetHistoryStoryRead()
        {
            var url = StoriesApiUrlDef.GetHistoryStoryRead();
            return await RequestAuthenGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện theo chủ đề
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>> GetAllStoryByTopic(Guid topicId)
        {
            var url = StoriesApiUrlDef.GetAllStoryByTopic(topicId);
            return await RequestGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>> GetNewVersionStoryByDay(DateTime dateTime)
        {
            var url = StoriesApiUrlDef.GetNewVervionStoryByDay(dateTime);
            return await RequestAuthenGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy detail truyện theo id
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<StoryDetailFullDTO> GetStoryById(Guid? id)
        {
            var url = StoriesApiUrlDef.GetStoryById(id);
            return await RequestGetAsync<StoryDetailFullDTO>(url);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện được tạo bởi user (chỉ cho tác giả)
        /// CreatedBy ntthe 14.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>> GetStoryByCurrentAuthor()
        {
            var url = StoriesApiUrlDef.GetStoryByCurrentAuthor();
            return await RequestAuthenGetAsync<List<StoryAccountGeneric>>(url);
        }

        /// <summary>
        /// Hàm xử lý thêm mới tác phẩm (master)
        /// CreatedBy ntthe 16.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> CreateStoryByAuthor(StoryRegisterVM storyRegister)
        {
            var url = StoriesApiUrlDef.CreateStoryByAuthor();
            return await RequestFullAuthenPostAsync<string>(url, storyRegister);
        }
    }
}
