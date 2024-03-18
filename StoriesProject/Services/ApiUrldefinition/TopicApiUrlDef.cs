namespace StoriesProject.Services.ApiUrldefinition
{
    public class TopicApiUrlDef
    {
        private static readonly string pathController = "/api/Topic";

        /// <summary>
        /// Tạo url lấy tất cả chủ đề
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetAllTopic()
        {
            return @$"{pathController}/GetAllTopic";
        }

        /// <summary>
        /// Tạo lấy tất cả chủ đề đủ thông tin
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetAllTopicInfor()
        {
            return @$"{pathController}/GetAllTopicInfor";
        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề sort theo số lượng truyện
        /// CreatedBy ntthe 16.03.2024
        /// </summary>
        /// <returns></returns>
        public static string GetAllTopicSortByStory()
        {
            return @$"{pathController}/GetAllTopicSortByStory";
        }

        /// <summary>
        /// Hàm xử lý thêm thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public static string AddTopic()
        {
            return @$"{pathController}/AddTopic";
        }

        /// <summary>
        /// Hàm xử lý xóa thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public static string DeleteTopic()
        {
            return @$"{pathController}/DeleteTopic";
        }

        /// <summary>
        /// Hàm xử lý sửa thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        public static string EditTopic()
        {
            return @$"{pathController}/EditTopic";
        }

    }
}
