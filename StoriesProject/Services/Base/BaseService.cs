using Newtonsoft.Json;
using StoriesProject.Common.Cache;
using StoriesProject.Model.ViewModel;
using StoriesProject.Services.ApiUrldefinition;
using System.Net;
using System.Net.Http.Headers;
using System.Text;

namespace StoriesProject.API.Services.Base
{
    public interface IBaseService
    {
        Task<T> RequestPostAsync<T>(string url, object model, string? accessToken = null);
        Task<T> RequestGetAsync<T>(string url);
    }

    public class BaseService : IBaseService
    {
        protected IDistributedCacheCustom _cache;
        private readonly string _remoteServiceBaseUrl;
        private IHttpClientFactory _httpClientFactory;
        public BaseService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
            _remoteServiceBaseUrl = config.GetSection("BaseUrlApi").Value;
        }

        public async Task<T> RequestPostAsync<T>(string url,object model, string? accessToken = null)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var jsonContent = JsonConvert.SerializeObject(model);
                    var content = new StringContent(jsonContent, Encoding.UTF8, "application/json");

                    // nếu có accessToken thì mới đưa Bearer Token vào
                    if (accessToken != null)
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }

                    // Thực hiện cuộc gọi API POST
                    var response = await httpClient.PostAsync(_remoteServiceBaseUrl + url, content);

                    // Kiểm tra xem cuộc gọi API có thành công không
                    response.EnsureSuccessStatusCode();

                    // Đọc nội dung phản hồi
                    var responseStr = await response.Content.ReadAsStringAsync();

                    // Giải mã phản hồi JSON
                    var responseObject = JsonConvert.DeserializeObject<ResponseOutput<T>>(responseStr);

                    if (responseObject != null && responseObject.IsSuccess)
                    {
                        return responseObject.Data ?? default(T);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }

        public async Task<T> RequestGetAsync<T>(string url)
        {
            try
            {
                var uri = _remoteServiceBaseUrl + AccountantApiUrlDef.Login();

                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    var response = await httpClient.GetAsync(_remoteServiceBaseUrl + url);

                    // Kiểm tra xem cuộc gọi API có thành công không
                    response.EnsureSuccessStatusCode();

                    // Đọc nội dung phản hồi
                    var responseStr = await response.Content.ReadAsStringAsync();

                    // Giải mã phản hồi JSON
                    var responseObject = JsonConvert.DeserializeObject<ResponseOutput<T>>(responseStr);
                    if (responseObject != null && responseObject.IsSuccess)
                    {
                        return responseObject.Data ?? default(T);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                return default(T);
            }
        }
    }
}
