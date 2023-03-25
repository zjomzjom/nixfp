using UrlShortener.WebApplication.Validators;
using Xunit;

namespace UrlShortener.WebApplication.Tests.Validators
{
    public class UrlValidatorTest
    {
        [Theory]
        [InlineData("http://google.com", true)]
        [InlineData("https://google.com", true)]
        [InlineData("https://maps.google.com", true)]
        [InlineData("https://maps.google.com/val", true)]
        [InlineData("https://maps.google.com/val?param1=dsa", true)]
        [InlineData("https://maps.google.com/val?param1=dsa&param2=cxz", true)]

        [InlineData("www.google.com", false)]
        [InlineData("google.com", false)]
        [InlineData("https://google", false)]
        [InlineData("https://google/asd", false)]
        [InlineData("https://google/asd?param1=dsa", false)]
        public void TestRegex(string url, bool expected)
        {
            var result = UrlValidator.Validate(url);
            Assert.Equal(expected, result);
        }
    }
}