using StoriesProject.API.Common.Repository;
using StoriesProject.API.Common.Ulti;
using StoriesProject.API.Model.BaseEntity;
using StoriesProject.API.Models.DTO;
using StoriesProject.API.Repositories.Base;

namespace StoriesProject.API.Repositories
{
    public interface IStoriesRepository : IBaseRepository<Story>
    {
        Task<IEnumerable<Story>?> GetTopStoryNew(int numberStory);
    }
    public class StoriesRepository : BaseRepository<Story>, IStoriesRepository
    {
        public StoriesRepository(IUnitOfWork entities):base(entities)
        {
            
        }

        public async Task<IEnumerable<Story>?> GetTopStoryNew(int numberStory)
        {
            var sortedList = new List<SortedPaging>
            {
                new SortedPaging
                {
                    Field = "CreatedDate",
                    IsAsc = false
                },
                new SortedPaging
                {
                    Field = "Price",
                    IsAsc = false
                }
            };

             var dataResult = await GetDataLimit(numberStory, sortedList);
            return dataResult;
        }
    }
}
