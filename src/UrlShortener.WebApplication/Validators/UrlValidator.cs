using System.Text.RegularExpressions;

namespace UrlShortener.WebApplication.Validators
{
    public static class UrlValidator
    {
        //taken from the internet
        const string UrlRegexPattern = @"^https?:\/\/(?:www\.)?[-a-zA-Z0-9@:%._\+~#=]{1,256}\.[a-zA-Z0-9()]{1,6}\b(?:[-a-zA-Z0-9()@:%_\+.~#?&\/=]*)$";

        public static bool Validate(string value) => Regex.IsMatch(value, UrlRegexPattern);
    }
}
