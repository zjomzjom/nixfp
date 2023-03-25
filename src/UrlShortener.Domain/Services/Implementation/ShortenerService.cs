using Microsoft.EntityFrameworkCore;
using UrlShortener.Data;
using UrlShortener.Data.Entities;
using UrlShortener.Domain.Generators;

namespace UrlShortener.Domain.Services.Implementation
{
    public class ShortenerService : IShortenerService
    {
        private readonly ShortenerDbContext dbContext;
        private readonly IKeyGenerator keyGenerator;

        public ShortenerService(ShortenerDbContext dbContext, IKeyGenerator keyGenerator)
        {
            this.dbContext = dbContext;
            this.keyGenerator = keyGenerator;
        }

        public async Task<ShorteningResult> Generate(string url)
        {
            var newResult = new ShorteningResult { Key = keyGenerator.GenerateKey(), TargetUrl = url };
            dbContext.ShorteningResults.Add(newResult);
            await dbContext.SaveChangesAsync();
            return newResult;
        }

        public async Task<ShorteningResult> Get(string key)
        {
            return await dbContext.ShorteningResults
                .AsNoTracking()
                .FirstOrDefaultAsync(r => r.Key == key);
        }

        public async Task<IReadOnlyCollection<ShorteningResult>> GetAll()
        {
            return await dbContext.ShorteningResults.ToListAsync();
        }
    }
}
