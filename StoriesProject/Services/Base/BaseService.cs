using Microsoft.AspNetCore.Authentication.Cookies;
using Newtonsoft.Json;
using StoriesProject.Common.Cache;
using StoriesProject.Model.ViewModel;
using StoriesProject.Services.ApiUrldefinition;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace StoriesProject.API.Services.Base
{
    public class BaseService
    {
        protected IDistributedCacheCustom _cache;
        private readonly string _remoteServiceBaseUrl;
        private readonly IHttpClientFactory _httpClientFactory;
        public BaseService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
            _remoteServiceBaseUrl = "";
        }
        public async Task<RestOutput?> RequestPostAsync<T>(T model, string accessToken)
        {
            try
            {
                var uri = _remoteServiceBaseUrl + AccountantApiUrlDef.Login();

                // Tạo một thể hiện của HttpClient bằng cách sử dụng factory
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    // Chuẩn bị nội dung yêu cầu (giả sử 'model' là một đối tượng bạn muốn gửi)
                    var jsonContent = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // Gán cookie từ đối tượng HttpClientHandler của HttpClient (nếu đã có cookie trước đó)
                    var cookieContainer = new CookieContainer();
                    var httpClientHandler = new HttpClientHandler { CookieContainer = cookieContainer };
                    httpClientHandler.UseCookies = true;
                    // hiện tại đang author bằng cookie nên lấy như này
                    httpClientHandler.CookieContainer.Add(new Uri(_remoteServiceBaseUrl), new Cookie(CookieAuthenticationDefaults.AuthenticationScheme, "YourCookieValue"));

                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);

                    // Thiết lập HttpClientHandler cho HttpClient
                    //httpClient.Handler = httpClientHandler;

                    // Thực hiện cuộc gọi API POST
                    var response = await httpClient.PostAsync(uri, content);

                    // Kiểm tra xem cuộc gọi API có thành công không
                    response.EnsureSuccessStatusCode();

                    // Đọc nội dung phản hồi
                    var responseStr = await response.Content.ReadAsStringAsync();

                    // Giải mã phản hồi JSON
                    var responseObject = JsonConvert.DeserializeObject<RestOutput>(responseStr);

                    return responseObject;
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

    }
}
