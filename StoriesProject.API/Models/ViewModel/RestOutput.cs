using System.Net;

namespace StoriesProject.API.Model.ViewModel
{
    public interface IRestOutput
    {
        void SuccessEventHandler(object data, string? message = null);
        void ErrorEventHandler(object data = null, string? message = "Đã có lỗi xảy ra");
        void ExceptionEventHandler();
    }
    public class RestOutput: IRestOutput
    {
        public HttpStatusCode StatusCode { get; set; }  // Mã trạng thái HTTP
        public string Message { get; set; }  // Thông điệp mô tả kết quả
        public object Data { get; set; }          // Dữ liệu trả về

        // Constructor để tạo một đối tượng RestOutput
        public RestOutput()
        {

        }

        public void SuccessEventHandler(object data = null, string? message = null)
        {
            StatusCode = HttpStatusCode.OK;
            Data = data;
            if (!string.IsNullOrEmpty(message)) 
            {
                Message = message;
            }
        }

        public void ErrorEventHandler(object data = null, string? message = "Đã có lỗi xảy ra")
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
