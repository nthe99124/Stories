using System.Net;

namespace StoriesProject.Model.ViewModel
{
    public interface IRestOutput
    {
        void SuccessEventHandler(object data, string? message = null);
        void ExceptionEventHandler();
    }
    public class RestOutput: IRestOutput
    {
        public HttpStatusCode StatusCode { get; set; }  // Mã trạng thái HTTP
        public string? Message { get; set; }  // Thông điệp mô tả kết quả
        public object? Data { get; set; }          // Dữ liệu trả về

        public void SuccessEventHandler(object? data = null, string? message = null)
        {
            StatusCode = HttpStatusCode.OK;
            Data = data;
            if (!string.IsNullOrEmpty(message)) 
            {
                Message = message;
            }
        }

        public void ErrorEventHandler(object? data, string? message = "Đã có lỗi xảy ra")
        {
            StatusCode = HttpStatusCode.OK;
            Data = data;
            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
        }

        public void ExceptionEventHandler()
        {
            StatusCode = HttpStatusCode.BadRequest;
            Message = "Đã có lỗi xảy ra";
        }
    }
}
