using Microsoft.Extensions.Caching.Distributed;

namespace StoriesProject.Common.Cache
{
    public interface IDistributedCacheCustom
    {
        Task SetStringAsync(string key, string value, TimeSpan? timeSpanCache = null, DistributedCacheEntryOptions? options = null, CancellationToken token = default(CancellationToken));
        Task<string> GetStringAsync(string key, CancellationToken token = default(CancellationToken));
        string GetString(string key);
        void SetString(string key, string value, TimeSpan? timeSpanCache = null, DistributedCacheEntryOptions? options = null, CancellationToken token = default(CancellationToken));
    }
    public class DistributedCacheCustom : IDistributedCacheCustom
    {
        private readonly IDistributedCache _cache;
        public DistributedCacheCustom(IDistributedCache cache)
        {
            _cache = cache;
        }

        /// <summary>
        /// Custom thêm hàm set để check trước
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpanCache"></param>
        /// <param name="options"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task SetStringAsync(string key, string value, TimeSpan? timeSpanCache = null, DistributedCacheEntryOptions? options = null, CancellationToken token = default(CancellationToken))
        {
            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }
            if (timeSpanCache == null && options.AbsoluteExpirationRelativeToNow == null)
            {
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // mặc định cache 5 phút
            }
            var cacheValue = await _cache.GetStringAsync(key);
            if(string.IsNullOrEmpty(cacheValue))
            {
                await _cache.SetStringAsync(key, value, options);
            }
        }

        /// <summary>
        /// Custom thêm hàm get tạm thời custom thôi
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<string> GetStringAsync(string key, CancellationToken token = default(CancellationToken))
        {
            return await _cache.GetStringAsync(key, token) ?? string.Empty;
        }

        /// <summary>
        /// Custom thêm hàm get (đồng bộ) tạm thời custom thôi
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            return _cache.GetString(key) ?? string.Empty;
        }

        /// <summary>
        /// Custom thêm hàm set (đồng bộ) để check trước
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="timeSpanCache"></param>
        /// <param name="options"></param>
        /// <param name="token"></param>
        public void SetString(string key, string value, TimeSpan? timeSpanCache = null, DistributedCacheEntryOptions? options = null, CancellationToken token = default(CancellationToken))
        {
            if (options == null)
            {
                options = new DistributedCacheEntryOptions();
            }
            if (timeSpanCache == null && options.AbsoluteExpirationRelativeToNow == null)
            {
                options.AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(5); // mặc định cache 5 phút
            }
            var cacheValue = GetString(key);
            if (string.IsNullOrEmpty(cacheValue))
            {
                _cache.SetString(key, value, options);
            }
        }
    }
}
