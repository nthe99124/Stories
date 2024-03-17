using Microsoft.JSInterop;
using StoriesProject.API.Services.Base;
using StoriesProject.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Story;
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

        /// <summary>
        /// Hàm xử lý thêm thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> AddTopic(string topicName)
        {
            var url = TopicApiUrlDef.AddTopic();
            return await RequestFullAuthenPostAsync<string>(url, topicName);
        }

        /// <summary>
        /// Hàm xử lý xóa thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> DeleteTopic(Guid topicId)
        {
            var url = TopicApiUrlDef.DeleteTopic();
            return await RequestFullAuthenPostAsync<string>(url, topicId);
        }

        /// <summary>
        /// Hàm xử lý xóa thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ResponseOutput<string>> EditTopic(Guid topicId, string topicName)
        {
            var editTopic = new EditTopicVM
            {
                TopicId = topicId,
                TopicName = topicName
            };
            var url = TopicApiUrlDef.EditTopic();
            return await RequestFullAuthenPostAsync<string>(url, editTopic);
        }
    }
}
