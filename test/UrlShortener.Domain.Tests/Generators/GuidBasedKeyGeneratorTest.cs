using UrlShortener.Domain.Generators.Implementation;
using Xunit;

namespace UrlShortener.Domain.Tests.Generators
{
    public class GuidBasedKeyGeneratorTest
    {
        private GuidBasedKeyGenerator sut;


        public GuidBasedKeyGeneratorTest()
        {
            sut = new GuidBasedKeyGenerator();
        }

        [Fact]
        public void GenerateKey_Should_GenerateDifferentValuesEveryTime()
        {
            var key1 = sut.GenerateKey();
            var key2 = sut.GenerateKey();
            var key3 = sut.GenerateKey();

            Assert.NotEqual(key1, key2);
            Assert.NotEqual(key1, key3);
            Assert.NotEqual(key2, key3);
        }
    }
}
