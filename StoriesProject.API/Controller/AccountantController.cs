using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Common.Attribute;
using StoriesProject.API.Common.Constant;
using StoriesProject.API.Services;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Accountant;

namespace StoriesProject.API.Controller.Base
{
    public class AccountantController : BaseController
    {
        private IAccountantsService _accoutantsService;
        public AccountantController(IRestOutput res, IHttpContextAccessor httpContextAccessor,
                                    IAccountantsService accoutantsService): base(res, httpContextAccessor)
        {
            _accoutantsService = accoutantsService;
        }

        /// <summary>
        /// Hàm xử lý đăng nhập
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="loginInfor"></param>
        /// <returns></returns>
        [HttpPost("Login")]
        public async Task<IActionResult> Login(LoginViewModel loginInfor)
        {
            try
            {
                var userResult = await _accoutantsService.Login(loginInfor.UserName, loginInfor.Password);
                if (userResult != null && !string.IsNullOrEmpty(userResult.Token))
                {
                    _res.SuccessEventHandler(userResult);
                }
            }
            catch (Exception ex)
            {
                _res.ErrorEventHandler(null, ex.Message);
            }

            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý đăng xuất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            _accoutantsService.Logout();
            _res.SuccessEventHandler("Đăng xuất thành công");
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý đăng ký
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("Register")]
        public async Task<IActionResult> Register(AccountantRegister acc)
        {
            if (acc != null)
            {
                var isSuccess = await _accoutantsService.Register(acc);
                if (isSuccess)
                {
                    _res.SuccessEventHandler("Đăng ký thành công");
                }
                else
                {
                    _res.ErrorEventHandler();
                }
                
            }
            return Ok(_res);
        }

        /// <summary>
        /// Đăng ký tác giả
        /// </summary>
        /// <returns></returns>
        [HttpPost("RegisterAuthorAccountant")]
        [Roles(RoleConstant.Customer)]
        public async Task<IActionResult> RegisterAuthorAccountant(AuthorRegisterModel register)
        {
            if (register != null)
            {
                var isSuccess = await _accoutantsService.RegisterAuthorAccountant(register);
                if (isSuccess)
                {
                    _res.SuccessEventHandler("Đăng ký thành công");
                }
                else
                {
                    _res.ErrorEventHandler();
                }

            }
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy thông tin user
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpGet("GetUserInforGeneric")]
        [Roles(RoleConstant.Customer, RoleConstant.Author)]
        public async Task<IActionResult> GetUserInforGeneric()
        {
            var userData = await _accoutantsService.GetUserInforGeneric();
            if (userData != null)
            {
                _res.SuccessEventHandler(userData);
            }
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý cập nhật thông tin user
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("UpdateUserInfor")]
        [Roles(RoleConstant.Customer, RoleConstant.Author)]
        public async Task<IActionResult> UpdateUserInfor(AccountantUpdate acc)
        {
            _res = await _accoutantsService.UpdateUserInfor(acc);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý cập nhật password
        /// CreatedBy ntthe 03.03.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("ChangePassword")]
        [Roles(RoleConstant.Customer, RoleConstant.Author)]
        public async Task<IActionResult> ChangePassword(ChangPasswordVM password)
        {
            _res = await _accoutantsService.ChangePassword(password.NewPassword, password.OldPassword);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lấy thông tin user
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <param name="acc"></param>
        /// <returns></returns>
        [HttpPost("GetUserInfor")]
        [Roles(RoleConstant.Customer, RoleConstant.Author)]
        public IActionResult GetUserInfor()
        {
            var userInfor = _accoutantsService.GetUserInfor();
            _res.SuccessEventHandler(userInfor);
            return Ok(_res);
        }

        /// <summary>
        /// Lấy toàn bộ danh sách user 
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAll")]
        [Roles(RoleConstant.Employee, RoleConstant.Admin)]
        public async Task<IActionResult> GetAll()
        {
            var userInfor = await _accoutantsService.GetAll();
            _res.SuccessEventHandler(userInfor);
            return Ok(_res);
        }

        /// <summary>
        /// Lấy danh sách user theo role
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetRegisterAccountantsByRole")]
        [Roles(RoleConstant.Employee, RoleConstant.Admin)]
        public async Task<IActionResult> GetRegisterAccountantsByRole()
        {
            var lstAccountants = await _accoutantsService.GetRegisterAccountantsByRole();
            _res.SuccessEventHandler(lstAccountants);
            return Ok(_res);
        }

        [HttpPost("ApprovedAccountant")]
        [Roles(RoleConstant.Employee, RoleConstant.Admin)]
        public async Task<IActionResult> ApprovedAccountant([FromBody]
        Guid regiserId)
        {
            var result = await _accoutantsService.ApprovedAccountant(regiserId);
            if (result != null && result.Value)
            {
                _res.SuccessEventHandler("Phê duyệt thành công");
            }
            else
            {
                _res.ErrorEventHandler("Phê duyệt thất bại");
            }

            return Ok(_res);
        }

        [HttpPost("DeniedAccountant")]
        [Roles(RoleConstant.Employee, RoleConstant.Admin)]
        public async Task<IActionResult> DeniedAccountant([FromBody]
        Guid regiserId)
        {
            var result = await _accoutantsService.DeniedAccountant(regiserId);
            if (result != null && result.Value)
            {
                _res.SuccessEventHandler("Từ chối thành công");
            }
            else
            {
                _res.ErrorEventHandler("Phê duyệt thất bại");
            }

            return Ok(_res);
        }

        [HttpPost("UpdateLockedAccountant")]
        [Roles(RoleConstant.Employee, RoleConstant.Admin)]
        public async Task<IActionResult> UpdateLockedAccountant(LockedAccountantParam param)
        {
            var result = await _accoutantsService.UpdateLockedAccountant(param);
            if (result != null && result.Value)
            {
                _res.SuccessEventHandler("Cập nhật trạng thái khóa tài khoản thành công");
            }
            else
            {
                _res.ErrorEventHandler("Cập nhật trạng thái khóa tài khoản thất bại");
            }

            return Ok(_res);
        }
    }
}
