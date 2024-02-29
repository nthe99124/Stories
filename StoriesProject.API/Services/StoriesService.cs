using StoriesProject.API.Common.Cache;
using StoriesProject.API.Model.BaseEntity;
using StoriesProject.API.Repositories;
using StoriesProject.API.Services.Base;

namespace StoriesProject.API.Services
{
    public interface IStoriesService
    {
        Task<IEnumerable<Story>?> GetTop10StoryNew();
    }
    public class StoriesService: BaseService, IStoriesService
    {
        private readonly IStoriesRepository _storiesRepository;
        public StoriesService(IHttpContextAccessor httpContextAccessor, IDistributedCacheCustom cache, IStoriesRepository storiesRepository) : base(httpContextAccessor, cache)
        {
            _storiesRepository = storiesRepository;
        }

        public async Task<IEnumerable<Story>?> GetTop10StoryNew()
        {
            return await _storiesRepository.GetTopStoryNew(10);
        }

        #region Private Method

        #endregion
    }
}
