namespace UrlShortener.WebApplication.Models
{
    public class ListResultsViewModel
    {
        public IReadOnlyCollection<ShorteningResultDTO> Results { get; init; }
    }
}
