using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface ILogEntryRepository : IBaseRepository<LogEntry>
    {
        
    }
    public class LogEntryRepository : BaseRepository<LogEntry>, ILogEntryRepository
    {
        public LogEntryRepository(StoriesContext context) : base(context)
        {

        }
    }
}
