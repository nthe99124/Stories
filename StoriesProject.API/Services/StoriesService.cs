using AutoMapper;
using StoriesProject.API.Common.Cache;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Services.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO;
using StoriesProject.Model.DTO.Story;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Story;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.API.Services
{
    public interface IStoriesService
    {
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10NewStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10HotStory(string? searchStory);
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10FreeStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10PaidStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetTop10NewVersionStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetHistoryStoryRead();
        Task<IEnumerable<StoryAccountGeneric>?> GetFavoriteStory();
        Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId, List<SortedPaging> sort);
        Task<IEnumerable<StoryAccountGeneric>?> GetNewVersionStoryByDay(DateTime dateTime);
        Task<StoryDetailFullDTO?> GetStoryById(Guid id);
        Task<IEnumerable<StoryAccountGeneric>?> GetStoryByCurrentAuthor();
        Task<Guid?> CreateStoryByAuthor(StoryRegisterVM storyRegister);
        Task<List<StoryAccountGeneric>?> GetTopPurchasesStory(Guid? topicId, int numberStory);
        Task<IEnumerable<StoryInforAdmin>?> GetListStoryForAdmin(StoryStatus status);
        Task<RestOutput> ChangeStatusStory(Guid storyId, StoryStatus status);
        Task<ContentChapterGeneric> GetContentChapter(Guid chapterId);
    }
    public class StoriesService: BaseService, IStoriesService
    {
        public StoriesService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IUnitOfWork unitOfWork, IMapper mapper) : base(httpContextAccessor, cache, unitOfWork, mapper)
        {

        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetTop10NewStory()
        {
            var top10Result = await _unitOfWork.StoriesRepository.GetTopNewStory(10);
            var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(top10Result);
            return result;
        }


        /// <summary>
        /// Hàm xử lý lấy 10 truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetTop10HotStory(string? name)
        {
            var top10Result = await _unitOfWork.StoriesRepository.GetTopHotStory(10, name);
            var result = _mapper.Map<IEnumerable<StoryAccountGeneric>>(top10Result);
            return result;
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện miễn phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetTop10FreeStory()
        {
            var top10Result = await _unitOfWork.StoriesRepository.GetTopFreeStory(10);
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
            var top10Result = await _unitOfWork.StoriesRepository.GetTopPaidStory(10);
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
            var topResult = await _unitOfWork.StoriesRepository.GetTopNewVersionStory(10);
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
            return await _unitOfWork.StoriesRepository.GetHistoryStoryRead((Guid)userId);
        }

        /// <summary>
        /// Hàm xử lý danh sách truyện yêu thích
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetFavoriteStory()
        {
            var userId = GetUserAuthen()?.AccoutantId;
            return await _unitOfWork.StoriesRepository.GetFavoriteStory((Guid)userId);
        }

        /// <summary>
        /// Hàm xử lý danh sách truyện theo chủ đề
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId, List<SortedPaging> sort)
        {
            return await _unitOfWork.StoriesRepository.GetAllStoryByTopic(topicId, sort);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetNewVersionStoryByDay(DateTime dateTime)
        {
            var topResult = await _unitOfWork.StoriesRepository.GetTopNewVersionStory(10);
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
            return await _unitOfWork.StoriesRepository.GetStoryById(id);
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
                var topResult = await _unitOfWork.StoriesRepository.GetStoryByAuthor((Guid)currentAuthorId);
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
        public async Task<Guid?> CreateStoryByAuthor(StoryRegisterVM storyRegister)
        {
            var currentAuthorId = GetUserAuthen()?.AccoutantId;
            if (storyRegister != null && currentAuthorId != null)
            {
                var storyInsert = _mapper.Map<Story>(storyRegister);
                // Thêm mới truyện
                await _unitOfWork.StoriesRepository.CreateAsync(storyInsert);

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
                        await _unitOfWork.TopicStoryRepository.CreateRangeAsync(lstTopicStory);
                    }
                }

                await _unitOfWork.CommitAsync();

                return storyInsert.Id;
            }
            return null;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách lượt mua cao nhất
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<List<StoryAccountGeneric>?> GetTopPurchasesStory(Guid? topicId, int numberStory)
        {
            var topResult = (from s in _unitOfWork.StoriesRepository.Get()
                         join ts in _unitOfWork.TopicStoryRepository.Get() on s.Id equals ts.StoryId into tmp_ts
                         from ts in tmp_ts.DefaultIfEmpty()
                         where ts == null || ts.TopicId == topicId
                         
                         orderby s.Purchases descending
                         select new StoryAccountGeneric
                         {
                             Id = s.Id,
                             Name = s.Name,
                             Code = s.Code,
                             ImageLink = s.ImageLink,
                             VideoLink = s.VideoLink,
                             Description = s.Description,
                             TypeOfStory = s.TypeOfStory,
                         }).Take(numberStory);
            var result = _mapper.Map<List<StoryAccountGeneric>>(topResult.ToList());
            return result;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện chờ xét duyệt
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<StoryInforAdmin>?> GetListStoryForAdmin(StoryStatus status)
        {
            var result = await _unitOfWork.StoriesRepository.GetListStoryForAdmin(status);
            return result;
        }

        /// <summary>
        /// Hàm xử lý thay đổi trạng thái duyệt của truyện
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<RestOutput> ChangeStatusStory(Guid storyId, StoryStatus status)
        {
            var res = new RestOutput();
            var story = await _unitOfWork.StoriesRepository.FirstOrDefault(item => item.Id == storyId);
            if (story != null)
            {
                story.Status = status;
                await _unitOfWork.CommitAsync();
                res.SuccessEventHandler("Đổi trạng thái thành công");
            }
            else 
            {
                res.ErrorEventHandler("Không tìm thấy truyện để đổi trạng thái");
            }
            return res;
        }

        /// <summary>
        /// Hàm xử lý thay đổi trạng thái duyệt của truyện
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public async Task<ContentChapterGeneric> GetContentChapter(Guid chapterId)
        {
            // ntthe 2 con này độc lập nên phân task
            var getStoryNameTask = Task.FromResult((from s in _unitOfWork.StoriesRepository.Get()
                                                     join c in _unitOfWork.ChapterRepository.Get() on s.Id equals c.StoryId
                                                     where c.Id == chapterId
                                                    select new ContentChapterGeneric
                                                    {
                                                        StoryName = s.Name,
                                                        ChapterName = c.Name,
                                                        StoryId = s.Id,
                                                    }).FirstOrDefault());
            var getChapterContentTask = _unitOfWork.ChapterContentRepository.FindBy(item => item.ChapterId == chapterId);
            await Task.WhenAll(getStoryNameTask, getChapterContentTask);
            var result = new ContentChapterGeneric();
            result = await getStoryNameTask;
            var chapterContentList = await getChapterContentTask;
            if (chapterContentList != null && chapterContentList.Count() > 0)
            {
                result.ImgLinkContent = chapterContentList.Select(item => item.ImgLink).ToList();
            }
            return result;
        }

        #region Private Method

        #endregion
    }
}