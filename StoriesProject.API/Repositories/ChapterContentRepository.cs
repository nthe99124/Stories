using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;

namespace StoriesProject.API.Repositories
{
    public interface IChapterContentRepository : IBaseRepository<ChapterContent>
    {
    }
    public class ChapterContentRepository : BaseRepository<ChapterContent>, IChapterContentRepository
    {
        public ChapterContentRepository(StoriesContext context) : base(context)
        {
            
        }
    }
}
