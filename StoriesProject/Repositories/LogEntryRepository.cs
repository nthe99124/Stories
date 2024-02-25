using StoriesProject.Common.Repository;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Repositories.Base;

namespace StoriesProject.Repositories
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
