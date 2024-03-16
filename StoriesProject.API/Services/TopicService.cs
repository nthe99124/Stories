using StoriesProject.API.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using StoriesProject.API.Common.Repository;
using System.Security.Cryptography;

namespace StoriesProject.API.Services
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>?> GetAllTopic();
        List<TopicGeneric>? GetAllTopicSortByStory();
    }
    public class TopicService : BaseService, ITopicService
    {
        public TopicService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IUnitOfWork unitOfWork, IMapper mapper) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {

        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề mới nhất
        /// CreatedBy ntthe 04.04.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Topic>?> GetAllTopic()
        {
            return await _unitOfWork.TopicRepository.GetAll();
        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề mới nhất
        /// CreatedBy ntthe 04.04.2024
        /// </summary>
        /// <returns></returns>
        public List<TopicGeneric>? GetAllTopicSortByStory()
        {
            var result = from t in _unitOfWork.TopicRepository.Get()
                         join ts in _unitOfWork.TopicStoryRepository.Get() on t.Id equals ts.TopicId into tmp_ts
                         from ts in tmp_ts.DefaultIfEmpty()
                         group ts by new { t.Id, t.Name } into g
                         orderby g.Count() descending
                         select new TopicGeneric
                         {
                             Id = g.Key.Id,
                             Name = g.Key.Name
                         };
            return result.ToList();
        }

        #region Private Method

        #endregion
    }
}
