using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface ITopicStoryRepository : IBaseRepository<TopicStory>
    {

    }
    public class TopicStoryRepository : BaseRepository<TopicStory>, ITopicStoryRepository
    {
        public TopicStoryRepository(IUnitOfWork entities):base(entities)
        {
            
        }
    }
}
