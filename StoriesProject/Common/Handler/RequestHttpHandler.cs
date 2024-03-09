using Microsoft.JSInterop;
using System.Net.Http.Headers;

namespace StoriesProject.Common.Handler
{
    public class RequestHttpHandler: DelegatingHandler
    {
        //private readonly IJSRuntime _js;
        //public RequestHttpHandler(IJSRuntime js)
        //{
        //    _js = js;
        //}
        protected override async Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            //var accessToken = await GetItemAsync("token");
            //if (!string.IsNullOrEmpty(accessToken))
            //{
            //    request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            //}
            return await base.SendAsync(request, cancellationToken);
        }

        //public async Task<string> GetItemAsync(string key)
        //{
        //    return await _js.InvokeAsync<string>("getSessionStorage", key);
        //}
    }
}
