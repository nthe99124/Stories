using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.Services.ApiUrldefinition
{
    public class StoriesApiUrlDef
    {
        private static readonly string pathController = "/api/Stories";

        /// <summary>
        /// Tạo url đăng nhập
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTop10NewStory()
        {
            return @$"{pathController}/GetTop10NewStory";
        }

        /// <summary>
        /// Tạo url lấy 10 truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTop10HotStory(string? name = null)
        {
            if (!string.IsNullOrEmpty(name))
            {
                return @$"{pathController}/GetTop10HotStory";
            }
            else
            {
                return @$"{pathController}/GetTop10HotStory?name={name}";
            }
            
        }

        /// <summary>
        /// Tạo url lấy 10 truyện miễn phi
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTop10FreeStory()
        {
            return @$"{pathController}/GetTop10FreeStory";
        }

        /// <summary>
        /// Tạo url lấy 10 truyện trả phi
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTop10PaidStory()
        {
            return @$"{pathController}/GetTop10PaidStory";
        }

        /// <summary>
        /// Tạo url lấy 10 truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTop10NewVervionStory()
        {
            return @$"{pathController}/GetTop10NewVervionStory";
        }

        /// <summary>
        /// Tạo url lấy 10 truyện yêu thích
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetFavoriteStory()
        {
            return @$"{pathController}/GetFavoriteStory";
        }

        /// <summary>
        /// Tạo url lấy 10 truyện đã đọc
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetHistoryStoryRead()
        {
            return @$"{pathController}/GetHistoryStoryRead";
        }

        /// <summary>
        /// Tạo url lấy 10 truyện theo chủ đề
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetAllStoryByTopic()
        {
            return @$"{pathController}/GetAllStoryByTopic";
        }

        /// <summary>
        /// Tạo url lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetNewVersionStoryByDay(DateTime dateTime)
        {
            string formattedDateTime = dateTime.ToString("yyyy-MM-ddTHH:mm:ss");
            string encodedDateTime = Uri.EscapeDataString(formattedDateTime);
            return @$"{pathController}/GetNewVersionStoryByDay?dateTime={encodedDateTime}";
        }

        /// <summary>
        /// Tạo url lấy detail truyện theo id
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetStoryById(Guid? id)
        {
            return @$"{pathController}/GetStoryById?id={id}";
        }

        /// <summary>
        /// Tạo url lấy danh sách truyện được tạo bởi user (chỉ cho tác giả)
        /// CreatedBy ntthe 14.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetStoryByCurrentAuthor()
        {
            return @$"{pathController}/GetStoryByCurrentAuthor";
        }

        /// <summary>
        /// Hàm xử lý thêm mới tác phẩm (master)
        /// CreatedBy ntthe 14.03.2024
        /// </summary>
        /// <returns></returns>
        public static string CreateStoryByAuthor()
        {
            return @$"{pathController}/CreateStoryByAuthor";
        }
        /// <summary>
        /// Tạo url lấy 10 truyện lượt bán cao nhất (theo topic)
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetTopPurchasesStory(Guid? topicId, int numberStory)
        {
            if (topicId != null)
            {
                return @$"{pathController}/GetTopPurchasesStory?numberStory={numberStory}&topicId={topicId}";
            }
            else
            {
                return @$"{pathController}/GetTopPurchasesStory?numberStory={numberStory}";
            }
            
        }

        /// <summary>
        /// Tạo url lấy content của chapter
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetContentChapter(Guid chapterId)
        {
            return @$"{pathController}/GetContentChapter?chapterId={chapterId}";
        }

        /// <summary>
        /// Tạo url xử lý thay đổi trạng thái duyệt của truyện
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public static string ChangeStatusStory()
        {
            return @$"{pathController}/ChangeStatusStory";
        }

        /// <summary>
        /// Tạo url xử lý lấy danh sách truyện chờ xét duyệt, đang hoạt động, từ chối
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetListStoryForAdmin(StoryStatus status)
        {
            return @$"{pathController}/ChangeStatusStory?status={status}";
        }
    }
}
