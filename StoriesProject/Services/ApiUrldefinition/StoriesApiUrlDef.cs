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
        public static string GetTop10HotStory()
        {
            return @$"{pathController}/GetTop10HotStory";
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
        
    }
}
