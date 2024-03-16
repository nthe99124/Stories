using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {
    }
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(StoriesContext context) : base(context)
        {
            
        }
    }
}
