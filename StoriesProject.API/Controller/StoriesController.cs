﻿using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Services;
using StoriesProject.Model.ViewModel;

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
        public async Task<IActionResult> GetTop10HotStory()
        {
            var dataResult = await _storiesService.GetTop10HotStory(); 
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
            var dataResult = await _storiesService.GetTop10NewVervionStory();
            _res.SuccessEventHandler(dataResult);
            return Ok(_res);
        }
    }
}
