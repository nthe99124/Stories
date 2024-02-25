using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel;
using StoriesProject.Services;

namespace StoriesProject.Controller.Base
{
    public class AccountantController : BaseController
    {
        private IAccoutantsService _accoutantsService;
        public AccountantController(RestOutput res, 
                                    IHttpContextAccessor httpContextAccessor, 
                                    IMapper mapper,
                                    IAccoutantsService accoutantsService): base(res, httpContextAccessor, mapper)
        {
            _accoutantsService = accoutantsService;
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var account = await _accoutantsService.Login(email, password);
            _res.SuccessEventHandler("Đăng nhập thành công");
            return Ok(_res);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _accoutantsService.Logout();
            _res.SuccessEventHandler("Đăng xuất thành công");
            return Ok(_res);
        }

        [HttpPost]
        public async Task<IActionResult> Register(string acc)
        {
            if (acc != null)
            {
                var accountObject = JsonConvert.DeserializeObject<Accountant>(acc);
                await _accoutantsService.Register(accountObject);
                _res.SuccessEventHandler("Đăng ký thành công");
            }
            return Ok(_res);
        }
    }
}
