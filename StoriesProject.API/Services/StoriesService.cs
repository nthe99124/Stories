using AutoMapper;
using StoriesProject.API.Common.Cache;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO.Story;

namespace StoriesProject.API.Services
{
    public interface IStoriesService
    {
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10NewStory();
        Task<IEnumerable<StoryGeneric>?> GetTop10HotStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10FreeStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10PaidStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10NewVersionStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetHistoryStoryRead();
        Task<IEnumerable<StoryAccountGeneric>?> GetFavoriteStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId);
        Task<IEnumerable<StoryAccountGeneric>?> GetNewVersionStoryByDay(DateTime dateTime);
        Task<StoryDetailFullDTO?> GetStoryById(Guid id);
        Task<IEnumerable<StoryAccountGeneric>?> GetStoryByCurrentAuthor();
        Task CreateStoryByAuthor(StoryRegisterVM storyRegister);
    }
    public class StoriesService: BaseService, IStoriesService
    {
        private readonly IStoriesRepository _storiesRepository;
        private readonly ITopicStoryRepository _topicStoryRepository;
        private readonly IMapper _mapper;
        public StoriesService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IStoriesRepository storiesRepository, IMapper mapper, ITopicStoryRepository topicStoryRepository) : base(httpContextAccessor, cache)
        {
            _storiesRepository = storiesRepository;
            _mapper = mapper;
            _topicStoryRepository = topicStoryRepository;
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetTop10NewStory()
        {
            var top10Result = await _storiesRepository.GetTopNewStory(10);
            var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(top10Result);
            return result;
        }


        /// <summary>
        /// Hàm xử lý lấy 10 truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryGeneric>?> GetTop10HotStory()
        {
            var top10Result = await _storiesRepository.GetTopHotStory(10);
            var result = _mapper.Map<IEnumerable<StoryGeneric>>(top10Result);
            return result;
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện miễn phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetTop10FreeStory()
        {
            var top10Result = await _storiesRepository.GetTopFreeStory(10);
            var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(top10Result);
            return result;
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện trả phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetTop10PaidStory()
        {
            var top10Result = await _storiesRepository.GetTopPaidStory(10);
            var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(top10Result);
            return result;
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetTop10NewVersionStory()
        {
            var topResult = await _storiesRepository.GetTopNewVersionStory(10);
            var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(topResult);
            return result;
        }

        /// <summary>
        /// Hàm xử lý danh sách lịch sử đọc
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetHistoryStoryRead()
        {
            var userId = GetUserAuthen()?.AccoutantId;
            return await _storiesRepository.GetHistoryStoryRead((Guid)userId);
        }

        /// <summary>
        /// Hàm xử lý danh sách truyện yêu thích
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetFavoriteStory()
        {
            var userId = GetUserAuthen()?.AccoutantId;
            return await _storiesRepository.GetFavoriteStory((Guid)userId);
        }

        /// <summary>
        /// Hàm xử lý danh sách truyện theo chủ đề
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId)
        {
            return await _storiesRepository.GetAllStoryByTopic(topicId);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetNewVersionStoryByDay(DateTime dateTime)
        {
            var topResult = await _storiesRepository.GetTopNewVersionStory(10);
            var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(topResult);
            return result;
        }

        /// <summary>
        /// Hàm xử lý lấy truyện theo id
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<StoryDetailFullDTO?> GetStoryById(Guid id)
        {
            return await _storiesRepository.GetStoryById(id);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện được tạo bởi user hiện tại (chỉ cho tác giả)
        /// CreatedBy ntthe 14.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetStoryByCurrentAuthor()
        {
            var currentAuthorId = GetUserAuthen()?.AccoutantId;
            if (currentAuthorId != null)
            {
                var topResult = await _storiesRepository.GetStoryByAuthor((Guid)currentAuthorId);
                var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(topResult);
                return result;
            }
            return null;
        }

        /// <summary>
        /// Hàm xử lý thêm mới tác phẩm (master)
        /// CreatedBy ntthe 15.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task CreateStoryByAuthor(StoryRegisterVM storyRegister)
        {
            var currentAuthorId = GetUserAuthen()?.AccoutantId;
            if (storyRegister != null && currentAuthorId != null)
            {
                var storyInsert = _mapper.Map<Story>(storyRegister);
                // Thêm mới truyện
                await _storiesRepository.InsertAsync(storyInsert);

                // Thêm mới bảng nhiều nhiều truyện và topic
                if (storyRegister.ListTopic != null && storyRegister.ListTopic.Count > 0)
                {
                    var lstTopicStory = new List<TopicStory>();
                    foreach (var item in storyRegister.ListTopic)
                    {
                        lstTopicStory.Add(new TopicStory()
                        {
                            TopicId = item,
                            StoryId = storyInsert.Id
                        });
                    }

                    if (lstTopicStory.Count > 0)
                    {
                        await _topicStoryRepository.InsertRangeAsync(lstTopicStory);
                    }
                }

                await _storiesRepository.Save();
            }
        }

        #region Private Method

        #endregion
    }
}