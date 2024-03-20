using Microsoft.AspNetCore.Components.Forms;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using StoriesProject.Common.Cache;
using StoriesProject.Model.ViewModel;
using StoriesProject.Services.ApiUrldefinition;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace StoriesProject.API.Services.Base
{
    public interface IBaseService
    {
        Task<T> RequestPostAsync<T>(string url, object model = null);
        Task<ResponseOutput<T>> RequestFullPostAsync<T>(string url, object model = null);
        Task<T> RequestGetAsync<T>(string url);
        Task<T> RequestAuthenPostAsync<T>(string url, object model = null);
        Task<ResponseOutput<T>> RequestFullAuthenPostAsync<T>(string url, object model = null);
        Task<T> RequestAuthenGetAsync<T>(string url);
        Task<ResponseOutput<T>> RequestFileAsync<T>(string url, List<IBrowserFile> selectedFile = null, object model = null);
    }

    public class BaseService : IBaseService
    {
        private readonly IJSRuntime _js;
        protected IDistributedCacheCustom _cache;
        private readonly string _remoteServiceBaseUrl;
        private IHttpClientFactory _httpClientFactory;
        
        public BaseService(IDistributedCacheCustom cache, IHttpClientFactory httpClientFactory, IConfiguration config, IJSRuntime js)
        {
            _cache = cache;
            _httpClientFactory = httpClientFactory;
            _remoteServiceBaseUrl = config.GetSection("BaseUrlApi").Value;
            _js = js;
        }

        public async Task<T> RequestPostAsync<T>(string url,object model = null)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {

                    var responseObject = await PostAsync<T>(httpClient, url, model, null);
                    if (responseObject != null && responseObject.IsSuccess)
                    {
                        return responseObject.Data ?? default(T);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseOutput<T>> RequestFullPostAsync<T>(string url, object model = null)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {

                    var responseObject = await PostAsync<T>(httpClient, url, model, null);

                    return responseObject;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> RequestAuthenPostAsync<T>(string url, object model = null)
        {
            try
            {
                var accessToken = await GetItemAsync("token");
                using (var httpClient = _httpClientFactory.CreateClient())
                {

                    var responseObject = await PostAsync<T>(httpClient, url, model, accessToken);
                    if (responseObject != null && responseObject.IsSuccess)
                    {
                        return responseObject.Data ?? default(T);
                    }

                    return default(T);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<ResponseOutput<T>> RequestFullAuthenPostAsync<T>(string url, object model = null)
        {
            try
            {
                var accessToken = await GetItemAsync("token");
                using (var httpClient = _httpClientFactory.CreateClient())
                {

                    var responseObject = await PostAsync<T>(httpClient, url, model, accessToken);

                    return responseObject;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<T> RequestAuthenGetAsync<T>(string url)
        {
            try
            {
                using (var httpClient = _httpClientFactory.CreateClient())
                {
                    httpClient.DefaultRequestHeaders.Accept.Clear();
                    httpClient.DefaultRequestHeaders.Clear();
                    httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    var accessToken = await GetItemAsync("token");
                    // nếu có accessToken thì mới đưa Bearer Token vào
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }

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
                throw ex;
            }
        }


        public async Task<T> RequestGetAsync<T>(string url)
        {
            try
            {
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
                throw ex;
            }
        }

        public async Task<ResponseOutput<T>> PostAsync<T>(HttpClient? httpClient, string url, object model = null, string? accessToken = null)
        {
            StringContent content = null;
            if (model != null)
            {
                var jsonContent = JsonConvert.SerializeObject(model);
                content = new StringContent(jsonContent, Encoding.UTF8, "application/json");
            }

            // nếu có accessToken thì mới đưa Bearer Token vào
            if (!string.IsNullOrEmpty(accessToken))
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

            return responseObject;
        }

        public async Task<ResponseOutput<T>> RequestFileAsync<T>(string url, List<IBrowserFile> selectedFile = null, object model = null)
        {
            using (var httpClient = new HttpClient())
            {
                using (var formData = new MultipartFormDataContent())
                {
                    if (model != null)
                    {
                        var jsonContent = JsonConvert.SerializeObject(model);
                        formData.Add(new StringContent(jsonContent, Encoding.UTF8, "application/json"));
                    }
                    // Thêm ảnh vào form data
                    foreach (var item in selectedFile)
                    {
                        formData.Add(new StreamContent(item.OpenReadStream()), "file", item.Name);
                    }
                    

                    var accessToken = await GetItemAsync("token");
                    // nếu có accessToken thì mới đưa Bearer Token vào
                    if (!string.IsNullOrEmpty(accessToken))
                    {
                        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
                    }
                    var response = await httpClient.PostAsync(_remoteServiceBaseUrl + url, formData);

                    // Kiểm tra xem cuộc gọi API có thành công không
                    response.EnsureSuccessStatusCode();

                    // Đọc nội dung phản hồi
                    var responseStr = await response.Content.ReadAsStringAsync();

                    // Giải mã phản hồi JSON
                    var responseObject = JsonConvert.DeserializeObject<ResponseOutput<T>>(responseStr);

                    return responseObject;
                }
            }
        }

        #region Đọc ghi sessionStorage
        public async Task SetItemAsync(string key, string value)
        {
            await _js.InvokeVoidAsync("setSessionStorage", key, value);
        }

        public async Task<string> GetItemAsync(string key)
        {
            return await _js.InvokeAsync<string>("getSessionStorage", key);
        }
        #endregion
    }
}
