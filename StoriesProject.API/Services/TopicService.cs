using StoriesProject.API.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services.Base;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using AutoMapper;
using StoriesProject.API.Common.Repository;
using System.Security.Cryptography;
using StoriesProject.Model.ViewModel;

namespace StoriesProject.API.Services
{
    public interface ITopicService
    {
        Task<IEnumerable<Topic>?> GetAllTopic();
        List<TopicGeneric>? GetAllTopicSortByStory();
        Task<RestOutput> AddTopic(string topicName);
        Task<RestOutput> EditTopic(Guid topicId, string topicName);
        Task<RestOutput> DeleteTopic(Guid topicId);
        List<TopicFullInfor>? GetAllTopicInfor();
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
        /// Hàm xử lý lấy all chủ đề sắp xếp theo số lượng truyện
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

        /// <summary>
        /// Hàm xử lý thêm thể loại
        /// CreatedBy ntthe 04.04.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> AddTopic(string topicName)
        {
            var res = new RestOutput();
            var topic = await _unitOfWork.TopicRepository.FirstOrDefault(item => item.Name == topicName);
            if (topic == null)
            {
                var currentUser = GetUserAuthen().AccoutantId;
                var topicNew = new Topic()
                {
                    Name = topicName,
                    CreatedBy = currentUser,
                };
                _unitOfWork.TopicRepository.Create(topicNew);
                await _unitOfWork.CommitAsync();
                res.SuccessEventHandler("Thêm mới thành công");
            }
            else
            {
                res.ErrorEventHandler("Thể loại đã tồn tại.");
            }
            return res;
        }

        /// <summary>
        /// Hàm xử lý xóa thể loại
        /// CreatedBy ntthe 04.04.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> DeleteTopic(Guid topicId)
        {
            var res = new RestOutput();
            var topic = await _unitOfWork.TopicRepository.FirstOrDefault(item => item.Id == topicId);
            if (topic != null)
            {
                // xóa dữ liệu trong topicstory
                var listTopicStory = await _unitOfWork.TopicStoryRepository.FindBy(item => item.TopicId == topicId);
                if (listTopicStory != null && listTopicStory.Count() >0)
                {
                    _unitOfWork.TopicStoryRepository.DeleteRange(listTopicStory);
                }

                // xóa tiếp dữ liệu trong topic
                _unitOfWork.TopicRepository.Delete(topic);

                await _unitOfWork.CommitAsync();
                res.SuccessEventHandler("Xóa thể loại thành công");
            }
            else
            {
                res.ErrorEventHandler("Thể loại không tồn tại.");
            }
            return res;
        }

        /// <summary>
        /// Hàm xử lý sửa thể loại
        /// CreatedBy ntthe 04.04.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> EditTopic(Guid topicId, string topicName)
        {
            var res = new RestOutput();
            var topic = await _unitOfWork.TopicRepository.FirstOrDefault(item => item.Id == topicId);
            if (topic != null)
            {
                topic.Name = topicName;
                await _unitOfWork.CommitAsync();
                res.SuccessEventHandler("Sửa thể loại thành công");
            }
            else
            {
                res.ErrorEventHandler("Thể loại không tồn tại.");
            }
            return res;
        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề và đủ thông tin
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public List<TopicFullInfor>? GetAllTopicInfor()
        {
            var result = from t in _unitOfWork.TopicRepository.Get()
                         join ts in _unitOfWork.TopicStoryRepository.Get() on t.Id equals ts.TopicId into tmp_ts
                         from ts in tmp_ts.DefaultIfEmpty()
                         group ts by new { t.Id, t.Name, t.CreatedDate, t.Description } into g
                         orderby g.Count() descending
                         select new TopicFullInfor
                         {
                             Id = g.Key.Id,
                             Name = g.Key.Name,
                             NumberStory = g.Count(),
                             CreatedDate = g.Key.CreatedDate,
                             Description = g.Key.Description,
                         };
            return result.ToList();
        }

        #region Private Method

        #endregion
    }
}
