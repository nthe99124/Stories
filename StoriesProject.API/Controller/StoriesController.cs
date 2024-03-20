using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using StoriesProject.API.Common.Attribute;
using StoriesProject.API.Common.Constant;
using StoriesProject.API.Services;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Story;
using static StoriesProject.Model.Enum.DataType;

namespace StoriesProject.API.Controller.Base
{
    public class StoriesController : BaseController
    {
        private IStoriesService _storiesService;
        public StoriesController(IRestOutput res, IHttpContextAccessor httpContextAccessor,
                                    IStoriesService storiesService) : base(res, httpContextAccessor)
        {
            _storiesService = storiesService;
        }

        /// <summary>
        /// Hàm xử lý lấy top 10 truyện mới nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10NewStory")]
        public async Task<IActionResult> GetTop10NewStory()
        {
            var dataResult = await _storiesService.GetTop10NewStory();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện hot nhất
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10HotStory")]
        public async Task<IActionResult> GetTop10HotStory(string? searchStory)
        {
            var dataResult = await _storiesService.GetTop10HotStory(searchStory); 
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện miễn phi
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10FreeStory")]
        public async Task<IActionResult> GetTop10FreeStory()
        {
            var dataResult = await _storiesService.GetTop10FreeStory();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện trả phi
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10PaidStory")]
        public async Task<IActionResult> GetTop10PaidStory()
        {
            var dataResult = await _storiesService.GetTop10PaidStory();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy 10 truyện mới cập nhật
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTop10NewVervionStory")]
        public async Task<IActionResult> GetTop10NewVervionStory()
        {
            var dataResult = await _storiesService.GetTop10NewVersionStory();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý danh sách lịch sử đọc
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetHistoryStoryRead")]
        [Roles(RoleConstant.Customer, RoleConstant.Author)]
        public async Task<IActionResult> GetHistoryStoryRead()
        {
            var dataResult = await _storiesService.GetHistoryStoryRead();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý danh sách truyện yêu thích
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetFavoriteStory")]
        [Roles(RoleConstant.Customer, RoleConstant.Author)]
        public async Task<IActionResult> GetFavoriteStory()
        {
            var dataResult = await _storiesService.GetFavoriteStory();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý danh sách truyện theo chủ đề
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("GetAllStoryByTopic")]
        public async Task<IActionResult> GetAllStoryByTopic(StoryByTopicParam param)
        {
            var dataResult = await _storiesService.GetAllStoryByTopic(param.TopicId, param.Sort);
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện cập nhật theo ngày
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetNewVersionStoryByDay")]
        public async Task<IActionResult> GetNewVersionStoryByDay(DateTime dateTime)
        {
            var dataResult = await _storiesService.GetNewVersionStoryByDay(dateTime);
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy truyện theo id
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStoryById")]
        public async Task<IActionResult> GetStoryById(Guid id)
        {
            var dataResult = await _storiesService.GetStoryById(id);
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện được tạo bởi user (chỉ cho tác giả)
        /// CreatedBy ntthe 14.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetStoryByCurrentAuthor")]
        [Roles(RoleConstant.Author)]
        public async Task<IActionResult> GetStoryByCurrentAuthor()
        {
            var dataResult = await _storiesService.GetStoryByCurrentAuthor();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý thêm mới tác phẩm (master)
        /// CreatedBy ntthe 16.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("CreateStoryByAuthor")]
        [Roles(RoleConstant.Author)]
        public async Task<IActionResult> CreateStoryByAuthor([FromForm] IFormFile file, [FromForm] string jsonRegister)
        {
            var storyRegister = JsonConvert.DeserializeObject<StoryRegisterVM>(jsonRegister);
            var data = await _storiesService.CreateStoryByAuthor(file, storyRegister);
            _res.SuccessEventHandler(data);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý danh sách 10 truyện lượt bán cao nhất (theo topic)
        /// CreatedBy ntthe 28.02.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTopPurchasesStory")]
        public async Task<IActionResult> GetTopPurchasesStory(Guid? topicId, int numberStory)
        {
            var dataResult = await _storiesService.GetTopPurchasesStory(topicId, numberStory);
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy danh sách truyện chờ xét duyệt, đang hoạt động, từ chối
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetListStoryForAdmin")]
        [Roles(RoleConstant.Employee, RoleConstant.Admin)]
        public async Task<IActionResult> GetListStoryForAdmin(StoryStatus status)
        {
            var dataResult = await _storiesService.GetListStoryForAdmin(status);
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý thay đổi trạng thái duyệt của truyện
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("ChangeStatusStory")]
        [Roles(RoleConstant.Employee, RoleConstant.Admin)]
        public async Task<IActionResult> ChangeStatusStory(ChangeStatusStoryVM changeStatusStory)
        {
            _res = await _storiesService.ChangeStatusStory(changeStatusStory.StoryId, changeStatusStory.Status);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý lấy content của chapter
        /// CreatedBy ntthe 17.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetContentChapter")]
        [Roles(RoleConstant.Customer)]
        public async Task<IActionResult> GetContentChapter(Guid chapterId)
        {
            var data = await _storiesService.GetContentChapter(chapterId);
            _res.SuccessEventHandler(data);
            return Ok(_res);
        }

        /// <summary>
        /// Hàm xử lý thêm mới chương
        /// CreatedBy ntthe 16.03.2024
        /// </summary>
        /// <returns></returns>
        [HttpPost("AddChapter")]
        [Roles(RoleConstant.Author)]
        public async Task<IActionResult> AddChapter([FromForm] List<IFormFile> file, [FromForm] string jsonChapter)
        {
            var chapter = JsonConvert.DeserializeObject<AddChapterVM>(jsonChapter);
            _res = await _storiesService.AddChapter(file, chapter);
            return Ok(_res);
        }
    }
}