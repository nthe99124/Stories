using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Model.BaseEntity;
using StoriesProject.API.Model.ViewModel;
using StoriesProject.API.Models.ViewModel.Accountant;
using StoriesProject.API.Services;

namespace StoriesProject.API.Controller.Base
{
    public class AccountantController : BaseController
    {
        private IAccoutantsService _accoutantsService;
        public AccountantController(IRestOutput res, 
                                    IHttpContextAccessor httpContextAccessor,
                                    IAccoutantsService accoutantsService): base(res, httpContextAccessor)
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
            var token = await _accoutantsService.Login(loginInfor.Email, loginInfor.Password);
            if (token != null)
            {
                _res.SuccessEventHandler(token);
                Response.Cookies.Append("access_token", token, new CookieOptions
                {
                    HttpOnly = true, // Set HttpOnly to true for security
                    Secure = true,   // Set Secure to true if your site uses HTTPS
                    SameSite = SameSiteMode.Strict, // Set SameSite to Strict for added security
                    Expires = DateTimeOffset.UtcNow.AddMinutes(15) // Set the expiration time
                });
            }
            else
            {
                _res.SuccessEventHandler("Sai tài khoản hoặc mật khẩu");
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
        public async Task<IActionResult> Register(Accountant acc)
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
    }
}
