using System.Net;

namespace StoriesProject.Model.ViewModel
{
    public interface IRestOutput
    {
        void SuccessEventHandler(object? data = null, string? message = null);
        void ErrorEventHandler(object? data = null, string message = "Đã có lỗi xảy ra");
    }
    public class RestOutput : IRestOutput
    {
        public bool IsSuccess { get; set; }  // Trạng thái thành công
        public string? Message { get; set; }  // Thông điệp mô tả kết quả
        public object? Data { get; set; } =  null;        // Dữ liệu trả về

        public void SuccessEventHandler(object? data = null, string? message = null)
        {
            IsSuccess = true;
            if (data != null)
            {
                Data = data;
            }
            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
        }

        public void ErrorEventHandler(object? data = null, string message = "Đã có lỗi xảy ra")
        {
            IsSuccess = false;
            if (data != null)
            {
                Data = data;
            }
            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
        }
    }
}
