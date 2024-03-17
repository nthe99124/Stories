using Dapper;
using Microsoft.Data.SqlClient;
using StoriesProject.API.Common.Repository;
using StoriesProject.API.Repositories.Base;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.DTO;
using StoriesProject.Model.DTO.Story;
using System.Linq.Expressions;
using static StoriesProject.Model.Enum.DataType;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace StoriesProject.API.Repositories
{
    public interface IStoriesRepository : IBaseRepository<Story>
    {
        Task<IEnumerable<Story>?> GetTopNewStory(int numberStory);
        Task<IEnumerable<Story>?> GetTopHotStory(int numberStory, string? searchStory);
        Task<IEnumerable<Story>?> GetTopFreeStory(int numberStory);
        Task<IEnumerable<Story>?> GetTopPaidStory(int numberStory);
        Task<IEnumerable<Story>?> GetTopNewVersionStory(int numberStory);
        Task<IEnumerable<StoryAccountGeneric>?> GetHistoryStoryRead(Guid accId);
        Task<IEnumerable<StoryAccountGeneric>?> GetFavoriteStory(Guid accId);
        Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId, List<SortedPaging> sort);
        Task<IEnumerable<Story>?> GetNewVersionStoryByDay(DateTime dateTime);
        Task<StoryDetailFullDTO?> GetStoryById(Guid storyId);
        Task<IEnumerable<Story>?> GetStoryByAuthor(Guid id);
        Task<IEnumerable<StoryInforAdmin>?> GetListStoryForAdmin(StoryStatus status);
    }
    public class StoriesRepository : BaseRepository<Story>, IStoriesRepository
    {
        public StoriesRepository(StoriesContext context) : base(context)
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
        public async Task<IEnumerable<Story>?> GetTopHotStory(int numberStory, string? searchStory)
        {
            var sortedList = new List<SortedPaging>
            {
                new SortedPaging
                {
                    Field = "ViewAccess",
                    IsAsc = false
                }
            };
            Expression<Func<Story, bool>> predicateFilter = null;
            if (!string.IsNullOrEmpty(searchStory))
            {
                predicateFilter = item => item.Name.Contains(searchStory);
            }

            var dataResult = await GetDataLimit(numberStory, sortedList, predicateFilter);
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
        public async Task<IEnumerable<Story>?> GetTopNewVersionStory(int numberStory)
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
            var storyAccount = ExecuteStoredProcedureObject<StoryAccountGeneric>("GetHistoryStoryRead", param);
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
            var storyAccount = ExecuteStoredProcedureObject<StoryAccountGeneric>("GetFavoriteStory", param);
            return storyAccount;
        }

        /// <summary>
        /// Hàm xử lý lấy detail truyện
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="storyId"></param>
        /// <returns></returns>
        public async Task<StoryDetailFullDTO?> GetStoryById(Guid storyId)
        {
            var param = new DynamicParameters();
            param.Add("@StoryId", storyId);

            //TODO: ntthe nghiên cứu dựng base cho thằng này
            var storyAccount =  ExecuteStoredProcedureMultiObject<StoryDetailDTO, ChapterslDTO, TopicslDTO>("GetStoryDetailById", param);
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
        public async Task<IEnumerable<StoryAccountGeneric>?> GetAllStoryByTopic(Guid topicId, List<SortedPaging> sort)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@TopicID", topicId),
            };
            var storyAccount = ExecuteStoredProcedureObject<StoryAccountGeneric>("GetAllStoryByTopic", param);
            if (sort != null && sort.Count > 0)
            {
                storyAccount = await GetDataBySorted<StoryAccountGeneric>(storyAccount, sort);
            }
            return storyAccount;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetNewVersionStoryByDay(DateTime dateTime)
        {
            var dataResult = await FindBy(item => (item.CreatedDate.HasValue && item.CreatedDate.Value.Date == dateTime.Date)
                                                    || (item.ModifiedDate.HasValue && item.ModifiedDate.Value.Date == dateTime.Date));
            return dataResult;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện theo tác giả tạo
        /// CreatedBy ntthe 14.03.2024
        /// </summary>
        /// <param name="numberStory"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Story>?> GetStoryByAuthor(Guid id)
        {
            var dataResult = await FindBy(item => (item.CreatedBy == id));
            return dataResult;
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện chờ xét duyệt
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <param name="StoryStatus status"></param>
        /// <returns></returns>
        public async Task<IEnumerable<StoryInforAdmin>?> GetListStoryForAdmin(StoryStatus status)
        {
            var param = new SqlParameter[]
            {
                new SqlParameter("@Status", status),
            };
            var lstStory = ExecuteStoredProcedureObject<StoryInforAdmin>("GetListStoryForAdmin", param);
            return lstStory;
        }
    }
}
