using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Services;
using StoriesProject.Model.ViewModel;

namespace StoriesProject.API.Controller.Base
{
    public class TopicController : BaseController
    {
        private ITopicService _topicService;
        public TopicController(IRestOutput res, IHttpContextAccessor httpContextAccessor,
                                    ITopicService topicService) : base(res, httpContextAccessor)
        {
            _topicService = topicService;
        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTopic")]
        public async Task<IActionResult> GetAllTopic()
        {
            var dataResult = await _topicService.GetAllTopic();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }
    }
}
