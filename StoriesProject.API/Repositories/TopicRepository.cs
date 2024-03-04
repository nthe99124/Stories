using Microsoft.Data.SqlClient;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO;
using System.Data;

namespace StoriesProject.API.Repositories
{
    public interface ITopicRepository : IBaseRepository<Topic>
    {
    }
    public class TopicRepository : BaseRepository<Topic>, ITopicRepository
    {
        public TopicRepository(IUnitOfWork entities):base(entities)
        {
            
        }
    }
}
