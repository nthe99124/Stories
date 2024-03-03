using System.Net;

namespace StoriesProject.Model.ViewModel
{
    public interface IResponseOutput<T>
    {
        void SuccessEventHandler(T data, string? message = null);
        void ErrorEventHandler(T data, string message = "Đã có lỗi xảy ra");
        void ExceptionEventHandler();
    }
    public class ResponseOutput<T> : IResponseOutput<T>
    {
        public HttpStatusCode StatusCode { get; set; }  // Mã trạng thái HTTP
        public string? Message { get; set; }  // Thông điệp mô tả kết quả
        public T? Data { get; set; } =  default!;        // Dữ liệu trả về

        public void SuccessEventHandler(T data = default!, string? message = null)
        {
            StatusCode = HttpStatusCode.OK;
            if (data != null)
            {
                Data = data;
            }
            if (!string.IsNullOrEmpty(message))
            {
                Message = message;
            }
        }

        public void ErrorEventHandler(T data = default!, string message = "Đã có lỗi xảy ra")
        {
            StatusCode = HttpStatusCode.OK;
            if (data != null)
            {
                Data = data;
            }
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
