using StoriesProject.API.Common.Repository;
using StoriesProject.API.Model.BaseEntity;
using StoriesProject.API.Repositories.Base;

namespace StoriesProject.API.Repositories
{
    public interface ILogEntryRepository : IBaseRepository<LogEntry>
    {
        
    }
    public class LogEntryRepository : BaseRepository<LogEntry>, ILogEntryRepository
    {
        public LogEntryRepository(IUnitOfWork entities) : base(entities)
        {

        }
    }
}
