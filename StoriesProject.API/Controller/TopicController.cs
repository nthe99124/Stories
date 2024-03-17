using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Services;
using StoriesProject.Model.BaseEntity;
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
        /// Hàm xử lý lấy all chủ đề
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

        /// <summary>
        /// Hàm xử lý lấy all chủ đề sort theo số lượng truyện
        /// CreatedBy ntthe 16.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTopicSortByStory")]
        public IActionResult GetAllTopicSortByStory()
        {
            var dataResult = _topicService.GetAllTopicSortByStory();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý thêm thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddTopic")]
        public async Task<IActionResult> AddTopic(string topicName)
        {
            _res = await _topicService.AddTopic(topicName);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý thêm thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("DeleteTopic")]
        public async Task<IActionResult> DeleteTopic(Guid topicId)
        {
            _res = await _topicService.DeleteTopic(topicId);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý sửa thể loại
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("EditTopic")]
        public async Task<IActionResult> EditTopic(Guid topicId, string topicName)
        {
            _res = await _topicService.EditTopic(topicId, topicName);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy all chủ đề và đủ thông tin
        /// CreatedBy ntthe 16.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetAllTopicInfor")]
        public IActionResult GetAllTopicInfor()
        {
            var dataResult = _topicService.GetAllTopicInfor();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }
    }
}
