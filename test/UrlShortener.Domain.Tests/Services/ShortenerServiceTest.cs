using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using UrlShortener.Data;
using UrlShortener.Data.Entities;
using UrlShortener.Domain.Generators;
using UrlShortener.Domain.Services.Implementation;
using Xunit;

namespace UrlShortener.Domain.Tests.Services
{
    public class ShortenerServiceTest : IDisposable
    {
        private const string TargetUri = "https://google.com";
        private const string Key = "key2";

        private ShortenerService sut;
        private ShortenerDbContext dbContext;
        private Mock<IKeyGenerator> keyGeneratorMock;

        public ShortenerServiceTest()
        {
            dbContext = TestsUtils.CreateInMemoryDbContext();
            
            keyGeneratorMock = new Mock<IKeyGenerator>();
            keyGeneratorMock.Setup(m => m.GenerateKey()).Returns(Key);

            sut = new ShortenerService(dbContext, keyGeneratorMock.Object);
        }

        [Fact]
        public async Task Generate_Should_GenerateProperKeyBasedOnGenerator()
        {
            var key = "newkey3";
            keyGeneratorMock.Setup(k => k.GenerateKey()).Returns(key);
            
            var actual = await sut.Generate(TargetUri);
            
            Assert.Equal(key, actual.Key);
        }

        [Fact]
        public async Task Generate_Should_StoreResultInDatabase()
        {
            await sut.Generate(TargetUri);
            
            var actual = dbContext.ShorteningResults.Single();
            
            Assert.Equal(Key, actual.Key);
            Assert.Equal(TargetUri, actual.TargetUrl);
        }

        [Fact]
        public async Task Get_Should_ReturnValueFromDatabase()
        {
            await dbContext.ShorteningResults.AddAsync(new ShorteningResult { Key = Key, TargetUrl = TargetUri});
            
            var actual = await sut.Get(Key);

            Assert.Equal(Key, actual.Key);
            Assert.Equal(TargetUri, actual.TargetUrl);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }
    }
}
