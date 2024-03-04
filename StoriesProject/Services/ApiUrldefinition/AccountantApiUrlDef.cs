namespace StoriesProject.Services.ApiUrldefinition
{
    public class AccountantApiUrlDef
    {
        private static readonly string pathController = "/api/Accountant";

        /// <summary>
        /// Tạo url đăng nhập
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        public static string Login()
        {
            return @$"{pathController}/Login";
        }

        /// <summary>
        /// Tạo url đăng ký
        /// CreatedBy ntthe 04.03.2024
        /// </summary>
        /// <returns></returns>
        public static string Register()
        {
            return @$"{pathController}/Register";
        }
    }
}
