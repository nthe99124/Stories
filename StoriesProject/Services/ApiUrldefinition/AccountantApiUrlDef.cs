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

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        public static string GetUserInforGeneric()
        {
            return @$"{pathController}/GetUserInforGeneric";
        }

        /// <summary>
        /// Lấy thông tin user
        /// </summary>
        public static string UpdateUserInfor()
        {
            return @$"{pathController}/UpdateUserInfor";
        }

        /// <summary>
        /// Cập nhật mật khẩu
        /// </summary>
        public static string ChangePassword()
        {
            return @$"{pathController}/ChangePassword";
        }

        /// <summary>
        /// Đăng ký tác giả
        /// </summary>
        public static string RegisterAuthorAccountant()
        {
            return @$"{pathController}/RegisterAuthorAccountant";
        }

        /// <summary>
        /// Lấy danh sách user theo role
        /// </summary>
        /// <returns></returns>
        public static string GetRegisterAccountantsByRole()
        {
            return @$"{pathController}/GetRegisterAccountantsByRole";
        }

        /// <summary>
        /// Phê duyệt quyền tác giả
        /// </summary>
        /// <returns></returns>
        public static string ApprovedAccountant()
        {
            return @$"{pathController}/ApprovedAccountant";
        }

        /// <summary>
        /// Từ chối phê duyệt quyền tác giả
        /// </summary>
        /// <returns></returns>
        public static string DeniedAccountant()
        {
            return @$"{pathController}/DeniedAccountant";
        }

        /// <summary>
        /// Lấy toàn bộ danh sách người dùng
        /// </summary>
        /// <returns></returns>
        public static string GetAllAccountants()
        {
            return @$"{pathController}/GetAll";
        }

        /// <summary>
        /// Cập nhật trạng thái khóa tài khoản
        /// </summary>
        /// <returns></returns>
        public static string UpdateLockedAccountant()
        {
            return @$"{pathController}/UpdateLockedAccountant";
        }

        /// <summary>
        /// Đăng xuất
        /// </summary>
        /// <returns></returns>
        public static string Logout()
        {
            return @$"{pathController}/Logout";
        }
    }
}
