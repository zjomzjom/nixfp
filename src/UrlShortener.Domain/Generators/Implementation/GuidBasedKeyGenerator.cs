namespace UrlShortener.Domain.Generators.Implementation
{
    public class GuidBasedKeyGenerator : IKeyGenerator
    {
        public string GenerateKey()
        {
            var guidHashCode = Guid.NewGuid().GetHashCode();
            return string.Format("{0:X}", guidHashCode).ToLower();
        }
    }
}
