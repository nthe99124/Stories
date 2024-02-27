namespace StoriesProject.Services.ApiUrldefinition
{
    public class AccountantApiUrlDef
    {
        private static readonly string pathController = "/api/Accountant";

        /// <summary>
        /// Tạo url đăng nhập
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public static string Login()
        {
            return @$"{pathController}/Login";
        }
    }
}
