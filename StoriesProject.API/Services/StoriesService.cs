using StoriesProject.API.Common.Cache;
using StoriesProject.Model.BaseEntity;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services.Base;

namespace StoriesProject.API.Services
{
    public interface IStoriesService
    {
        Task<IEnumerable<Story>?> GetTop10NewStory();
        Task<IEnumerable<Story>?> GetTop10HotStory();
        Task<IEnumerable<Story>?> GetTop10FreeStory();
        Task<IEnumerable<Story>?> GetTop10PaidStory();

        Task<IEnumerable<Story>?> GetTop10NewVervionStory();
    }
    public class StoriesService: BaseService, IStoriesService
    {
        private readonly IStoriesRepository _storiesRepository;
        public StoriesService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IStoriesRepository storiesRepository) : base(httpContextAccessor, cache)
        {
            _storiesRepository = storiesRepository;
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTop10NewStory()
        {
            return await _storiesRepository.GetTopNewStory(10);
        }


        /// <summary>
        /// Hàm xử lý lấy 10 truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTop10HotStory()
        {
            return await _storiesRepository.GetTopHotStory(10);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện miễn phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTop10FreeStory()
        {
            return await _storiesRepository.GetTopFreeStory(10);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện trả phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTop10PaidStory()
        {
            return await _storiesRepository.GetTopPaidStory(10);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTop10NewVervionStory()
        {
            return await _storiesRepository.GetTopNewVervionStory(10);
        }

        #region Private Method

        #endregion
    }
}
