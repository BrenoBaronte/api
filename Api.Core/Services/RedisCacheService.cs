using Api.Domain.Services;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Threading.Tasks;

namespace Api.Core.Services
{
    public class RedisCacheService : ICacheService
    {
        public string CacheConnectionString { get; }
        public Lazy<ConnectionMultiplexer> Connection { get; }
        public IDatabase Cache { get; private set; }

        public RedisCacheService(string cacheConnectionString)
        {
            CacheConnectionString = cacheConnectionString
                ?? throw new ArgumentNullException(nameof(cacheConnectionString));
            Connection = new Lazy<ConnectionMultiplexer>(() =>
            {
                return ConnectionMultiplexer.Connect(cacheConnectionString);
            });
            Cache = Connection.Value.GetDatabase();
        }

        public async Task<T> GetAsync<T>(string key) where T : class
        {
            var cachedObject = await Cache.StringGetAsync(key);

            if (cachedObject.IsNull)
                return null;

            var deserializedObject = JsonConvert.DeserializeObject<T>(cachedObject);

            return deserializedObject;
        }

        public async Task SetAsync<T>(string key, object obj) where T : class
        {
            var objectToCache = JsonConvert.SerializeObject(obj);

            await Cache.StringSetAsync(key, objectToCache, new TimeSpan(0, 10, 0));
        }
    }
}
