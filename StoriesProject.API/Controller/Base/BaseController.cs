using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Model.ViewModel;

namespace StoriesProject.API.Controller.Base
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IRestOutput _res;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        public BaseController(IRestOutput res, IHttpContextAccessor httpContextAccessor)
        {
            _res = res;
            _httpContextAccessor = httpContextAccessor;
        }

        #region Protected Method

        #endregion
    }
}
