using Microsoft.AspNetCore.Mvc;
using StoriesProject.API.Common.Attribute;
using StoriesProject.API.Common.Constant;
using StoriesProject.API.Common.Ulti;
using StoriesProject.API.Services;
using StoriesProject.Model.BaseEntity;
using StoriesProject.Model.ViewModel;
using StoriesProject.Model.ViewModel.Story;

namespace StoriesProject.API.Controller.Base
{
    public class FileController : BaseController
    {
        private readonly IFileUlti _fileUlti;
        public FileController(IRestOutput res, IHttpContextAccessor httpContextAccessor, IFileUlti fileUlti) : base(res, httpContextAccessor)
        {
            _fileUlti = fileUlti;
        }

        [HttpGet]
        [Route("images/{imageName}")]
        public async Task<IActionResult> GetImage(string imageName)
        {
            var imageFileStream = await _fileUlti.ReadFile(imageName);
            return File(imageFileStream, "image/jpeg");
        }
    }
}
