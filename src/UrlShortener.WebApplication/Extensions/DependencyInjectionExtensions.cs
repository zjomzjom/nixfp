using FluentValidation;
using Serilog;
using UrlShortener.Domain.Generators;
using UrlShortener.Domain.Generators.Implementation;
using UrlShortener.Domain.Services;
using UrlShortener.Domain.Services.Implementation;
using UrlShortener.Domain.Utils;
using UrlShortener.WebApplication.Models;
using UrlShortener.WebApplication.Validators;

namespace UrlShortener.WebApplication.Configuration
{
    public static class DependencyInjectionExtensions
    {
        public static IServiceCollection AddCoreComponents(this IServiceCollection services)
        {
            services.AddScoped<IShortenerService, ShortenerService>();
            services.Decorate<IShortenerService, ShortenerServiceCacheDecorator>();

            services.AddSingleton<IKeyGenerator, GuidBasedKeyGenerator>();
            services.AddSingleton<IValidator<ShorteningViewModel>, ShortenerDTOValidator>();

            return services;
        }

        public static IServiceCollection AddSerilog(this IServiceCollection services, IConfiguration configuration)
        {
            var logger = new LoggerConfiguration().ReadFrom.Configuration(configuration).CreateLogger();
            services.AddSingleton(logger);
            return services;
        }
    }
}
