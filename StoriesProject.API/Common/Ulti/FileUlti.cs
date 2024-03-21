namespace StoriesProject.API.Common.Ulti
{
    public interface IFileUlti
    {
        Task<string> SaveFile(IFormFile file);
        Task<FileStream> ReadFile(string fileName);
    }
    public class FileUlti : IFileUlti
    {
        private readonly IWebHostEnvironment _hostingEnvironment;
        public FileUlti(IWebHostEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        public async Task<string> SaveFile(IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                throw new ArgumentException("Invalid file.");
            }

            // Tạo một tên file duy nhất bằng cách kết hợp tên và định dạng mở rộng
            var fileName = file.FileName;

            // Đường dẫn đến thư mục lưu trữ file (ví dụ: wwwroot/uploads)
            var uploadFolder = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads");

            // Tạo thư mục lưu trữ file nếu nó không tồn tại
            if (!Directory.Exists(uploadFolder))
            {
                Directory.CreateDirectory(uploadFolder);
            }

            // Đường dẫn đến file lưu trữ trên server
            var filePath = Path.Combine(uploadFolder, file.FileName);

            if (!File.Exists(filePath))
            {
                // Mở luồng để ghi dữ liệu file từ yêu cầu vào file trên server
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }

            return fileName; // Trả về tên file đã lưu
        }

        public async Task<FileStream> ReadFile(string fileName)
        {
            // Đường dẫn đến file trên server
            var filePath = Path.Combine(_hostingEnvironment.ContentRootPath, "uploads", fileName);
            var imageFileStream = System.IO.File.OpenRead(filePath);
            // Đọc dữ liệu từ file
            return imageFileStream;
        }
    }
}
