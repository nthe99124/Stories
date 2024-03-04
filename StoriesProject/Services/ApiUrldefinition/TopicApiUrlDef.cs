namespace StoriesProject.Services.ApiUrldefinition
{
    public class TopicApiUrlDef
    {
        private static readonly string pathController = "/api/Topic";

        /// <summary>
        /// Tạo url đăng nhập
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string GetAllTopic()
        {
            return @$"{pathController}/GetAllTopic";
        }
    }
}
