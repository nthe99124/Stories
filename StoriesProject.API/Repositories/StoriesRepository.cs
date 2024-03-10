using Dapper;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Linq;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO;
using StoriesProject.Model.DTO.Story;
using System.Data;

namespace StoriesProject.API.Repositories
{
    public interface IStoriesRepository : IBaseRepository<Story>
    {
        Task<IEnumerable<Story>?> GetTopNewStory(int numberStory);
        Task<IEnumerable<Story>?> GetTopHotStory(int numberStory);
        Task<IEnumerable<Story>?> GetTopFreeStory(int numberStory);
        Task<IEnumerable<Story>?> GetTopPaidStory(int numberStory);
        Task<IEnumerable<Story>?> GetTopNewVervionStory(int numberStory);
        Task<IEnumerable<StoryAccountGeneric>?> GetHistoryStoryRead(Guid accId);
        Task<IEnumerable<StoryAccountGeneric>?> GetFavoriteStory(Guid accId);
        Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId);
        Task<IEnumerable<Story>?> GetNewVervionStoryByDay(DateTime dateTime);
        Task<StoryDetailFullDTO?> GetStoryById(Guid topicId);
    }
    public class StoriesRepository : BaseRepository<Story>, IStoriesRepository
    {
        public StoriesRepository(IUnitOfWork entities):base(entities)
        {
            
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTopNewStory(int numberStory)
        {
            var sortedList = new List<SortedPaging>
            {
                new SortedPaging
                {
                    Field = "CreatedDate",
                    IsAsc = false
                }
            };

             var dataResult = await GetDataLimit(numberStory, sortedList);
            return dataResult;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTopHotStory(int numberStory)
        {
            var sortedList = new List<SortedPaging>
            {
                new SortedPaging
                {
                    Field = "ViewAccess",
                    IsAsc = false
                }
            };

            var dataResult = await GetDataLimit(numberStory, sortedList);
            return dataResult;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện miễn phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTopFreeStory(int numberStory)
        {
            var dataResult = await GetDataLimit(numberStory, null, item => item.Price == 0);
            return dataResult;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện trả phí
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTopPaidStory(int numberStory)
        {
            var dataResult = await GetDataLimit(numberStory, null, item => item.Price != 0);
            return dataResult;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetTopNewVervionStory(int numberStory)
        {
            var sortedList = new List<SortedPaging>
            {
                new SortedPaging
                {
                    Field = "ModifiedDate",
                    IsAsc = false
                }
            };
            var dataResult = await GetDataLimit(numberStory, null, item => item.ModifiedDate != null);
            return dataResult;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách lịch sử truyện đã đọc
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetHistoryStoryRead(Guid accId)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@AccountantID", accId)
            };
            var storyAccount = _entities.ExecuteStoredProcedureObject<StoryAccountGeneric>("GetHistoryStoryRead", param);
            return storyAccount;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện yêu thích
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetFavoriteStory(Guid accId)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@AccountantID", accId)
            };
            var storyAccount = _entities.ExecuteStoredProcedureObject<StoryAccountGeneric>("GetFavoriteStory", param);
            return storyAccount;
        }

        /// <summary>
        /// Hàm xử lý lấy detail truyện
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public async Task<StoryDetailFullDTO?> GetStoryById(Guid topicId)
        {
            var param = new DynamicParameters();
            param.Add("@TopicID", topicId);

            //TODO: ntthe nghiên cứu dựng base cho thằng này
            var storyAccount = _entities.ExecuteStoredProcedureMultiObject<StoryDetailDTO, ChapterslDTO, TopicslDTO>("GetAllStoryByTopic", param);
            var detailData = new StoryDetailFullDTO()
            {
                DetailStory = storyAccount.Item1.FirstOrDefault(),
                Chapter = storyAccount.Item2,
                Topic = storyAccount.Item3,
            };
            return detailData;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện theo chủ đề
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="accId"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@TopicID", topicId)
            };
            var storyAccount = _entities.ExecuteStoredProcedureObject<StoryAccountGeneric>("GetAllStoryByTopic", param);
            return storyAccount;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetNewVervionStoryByDay(DateTime dateTime)
        {
            var dataResult = await FindBy(item => (item.CreatedDate.HasValue && item.CreatedDate.Value.Date == dateTime.Date)
                                                    || (item.ModifiedDate.HasValue && item.ModifiedDate.Value.Date == dateTime.Date));
            return dataResult;
        }
    }
}
