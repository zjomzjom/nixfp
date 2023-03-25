using UrlShortener.Data.Entities;

namespace UrlShortener.Domain.Services
{
    public interface IShortenerService
    {
        Task<ShorteningResult> Generate(string url);

        Task<ShorteningResult> Get(string key);

        Task<IReadOnlyCollection<ShorteningResult>> GetAll();
    }
}
