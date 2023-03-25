using Microsoft.Extensions.Caching.Memory;
using Moq;
using System.Threading.Tasks;
using UrlShortener.Data.Entities;
using UrlShortener.Domain.Services;
using UrlShortener.Domain.Utils;
using Xunit;

namespace UrlShortener.Domain.Tests.Utils
{
    public class ShortenerServiceCacheDecoratorTest
    {
        const string TargetUri = "https://source.dot.net";
        const string Key = "key1";

        private ShortenerServiceCacheDecorator sut;
        private Mock<IShortenerService> shortenerServiceMock;
        private Mock<IMemoryCache> memoryCacheMock;

        public ShortenerServiceCacheDecoratorTest()
        {
            shortenerServiceMock = new Mock<IShortenerService>();
            memoryCacheMock = new Mock<IMemoryCache>();
            memoryCacheMock.Setup(m => m.CreateEntry(It.IsAny<object>())).Returns(new Mock<ICacheEntry>().Object);

            sut = new ShortenerServiceCacheDecorator(shortenerServiceMock.Object, memoryCacheMock.Object);
        }

        [Fact]
        public async Task Generate_Should_CallTargetServiceAndSetResultInCache()
        {
            var result = new ShorteningResult { Key = Key, TargetUrl = TargetUri };
            shortenerServiceMock.Setup(m => m.Generate(TargetUri)).ReturnsAsync(result);

            var actual = await sut.Generate(TargetUri);

            Assert.Equal(result, actual);
            memoryCacheMock.Verify(m => m.CreateEntry(Key), Times.Once);
        }

        [Fact]
        public async Task Get_When_ValueExistsInCache_Should_ReturnValueFromCache()
        {
            var result = new ShorteningResult { Key = Key, TargetUrl = TargetUri };
            memoryCacheMock.Setup(m => m.TryGetValue(Key, out It.Ref<object>.IsAny)).Callback((object key, out object value) => { value = result; }).Returns(true);

            var actual = await sut.Get(Key);

            Assert.Equal(result, actual);
            shortenerServiceMock.Verify(m => m.Get(Key), Times.Never);
        }

        [Fact]
        public async Task Get_When_ValueDoesNotExistInCache_Should_ReturnValueFromTargetServiceAndSetResultInCache()
        {
            var result = new ShorteningResult { Key = Key, TargetUrl = TargetUri };
            memoryCacheMock.Setup(m => m.TryGetValue(Key, out It.Ref<object>.IsAny)).Returns(false);
            shortenerServiceMock.Setup(m => m.Get(Key)).ReturnsAsync(result);

            var actual = await sut.Get(Key);

            Assert.Equal(result, actual);
            memoryCacheMock.Verify(m => m.TryGetValue(Key, out It.Ref<object>.IsAny), Times.Once);
            shortenerServiceMock.Verify(m => m.Get(Key), Times.Once);
        }

    }
}
