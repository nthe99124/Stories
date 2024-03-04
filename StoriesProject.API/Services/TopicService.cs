using StoriesProject.API.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services.Base;

namespace StoriesProject.API.Services
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>?> GetAllTopic();
    }
    public class TopicService : BaseService, ITopicService
    {
        private readonly ITopicRepository _topicRepository;
        public TopicService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, ITopicRepository topicRepository) : base(httpContextAccessor, cache)
        {
            _topicRepository = topicRepository;
        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề mới nhất
        /// CreatedBy ntthe 04.04.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Topic>?> GetAllTopic()
        {
            return await _topicRepository.GetAll();
        }
        
        #region Private Method

        #endregion
    }
}
