﻿using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace StoriesProject.Common.Cache
{
    public interface IDistributedCacheCustom
    {
        Task SetStringAsync(string key, string value, TimeSpan? timeSpanCache = null, DistributedCacheEntryOptions? options = null, CancellationToken token = default(CancellationToken));
        Task<T?> GetValueCacheAsync<T>(string key, CancellationToken token = default(CancellationToken));
        T? GetValueCache<T>(string key);
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
        /// Custom thêm hàm get lấy value object
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="key"></param>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<T?> GetValueCacheAsync<T>(string key, CancellationToken token = default(CancellationToken))
        {
            var cacheValue = await _cache.GetStringAsync(key);
            if (cacheValue != null)
            {
                return JsonSerializer.Deserialize<T>(cacheValue);
            }
            else
            {
                return default(T);
            }
        }

        /// <summary>
        /// Custom thêm hàm get lấy value object
        /// CreatedBy ntthe 25.02.2024
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public T? GetValueCache<T>(string key)
        {
            var cacheValue = _cache.GetString(key);
            if (cacheValue != null)
            {
                return JsonSerializer.Deserialize<T>(cacheValue);
            }
            else
            {
                return default(T);
            }
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
            var cacheValue = _cache.GetString(key);
            if (string.IsNullOrEmpty(cacheValue))
            {
                _cache.SetString(key, value, options);
            }
        }
    }
}
