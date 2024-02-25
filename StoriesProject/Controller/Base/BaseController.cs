using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using StoriesProject.Model.ViewModel;

namespace StoriesProject.Controller.Base
{
    public class BaseController : ControllerBase
    {
        protected readonly RestOutput _res;
        protected readonly IHttpContextAccessor _httpContextAccessor;
        protected readonly IMapper _mapper;
        public BaseController(RestOutput res, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _res = res;
            _httpContextAccessor = httpContextAccessor;
            _mapper = mapper;
        }

        #region Protected Method

        #endregion
    }
}
