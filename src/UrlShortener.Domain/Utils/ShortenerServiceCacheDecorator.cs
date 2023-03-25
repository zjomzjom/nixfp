using Microsoft.Extensions.Caching.Memory;
using UrlShortener.Data.Entities;
using UrlShortener.Domain.Services;
using UrlShortener.Domain.Services.Implementation;

namespace UrlShortener.Domain.Utils
{
    public class ShortenerServiceCacheDecorator : IShortenerService
    {
        private readonly IShortenerService shortenerService;
        private readonly IMemoryCache cache;

        public ShortenerServiceCacheDecorator(IShortenerService shortenerService, IMemoryCache cache)
        {
            this.shortenerService = shortenerService;
            this.cache = cache;
        }

        public async Task<ShorteningResult> Generate(string url)
        {
            var result = await shortenerService.Generate(url);
            cache.Set(result.Key, result);
            return result;
        }

        public async Task<ShorteningResult> Get(string key)
        {
            if (cache.TryGetValue<ShorteningResult>(key, out var result))
                return result;

            result = await shortenerService.Get(key);
            if (result != null)
                cache.Set(key, result);

            return result;
        }

        public async Task<IReadOnlyCollection<ShorteningResult>> GetAll()
        {
            return await shortenerService.GetAll();
        }
    }
}
