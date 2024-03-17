using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface IChapterRepository : IBaseRepository<Chapter>
    {
    }
    public class ChapterRepository : BaseRepository<Chapter>, IChapterRepository
    {
        public ChapterRepository(StoriesContext context) : base(context)
        {
            
        }
    }
}
