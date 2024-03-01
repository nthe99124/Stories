using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Model.ViewModel;
using StoriesProject.API.Services;

namespace StoriesProject.API.Controller.Base
{
    public class StoriesController : BaseController
    {
        private IStoriesService _storiesService;
        public StoriesController(IRestOutput res, 
                                    IHttpContextAccessor httpContextAccessor,
                                    IStoriesService storiesService) : base(res, httpContextAccessor)
        {
            _storiesService = storiesService;
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10StoryNew")]
        public async Task<IActionResult> GetTop10StoryNew()
        {
            var dataResult = await _storiesService.GetTop10StoryNew(); 
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }
    }
}
