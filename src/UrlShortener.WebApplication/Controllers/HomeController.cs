using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Serilog.Core;
using UrlShortener.Domain.Services;
using UrlShortener.WebApplication.Models;

namespace UrlShortener.WebApplication.Controllers
{
    public class HomeController : Controller
    {
        private const string RedirectActionName = "redirect";

        private readonly Logger logger;
        private readonly IShortenerService shortenerService;
        private readonly IValidator<ShorteningViewModel> validator;
        private readonly LinkGenerator linkGenerator;

        public HomeController(Logger logger, IShortenerService shortenerService, IValidator<ShorteningViewModel> validator, LinkGenerator linkGenerator)
        {
            this.logger = logger;
            this.shortenerService = shortenerService;
            this.validator = validator;
            this.linkGenerator = linkGenerator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("generate")]
        public async Task<IActionResult> Generate([FromForm] ShorteningViewModel model)
        {
            var validationResult = validator.Validate(model);
            if (validationResult.IsValid)
            {
                var result = await shortenerService.Generate(model.TargetUrl);
                var resultUrl = linkGenerator.GetUriByRouteValues(HttpContext, routeName: RedirectActionName, values: new { key = result.Key })?.ToString();
                
                logger.Information("Shortening succeeded. Key {Key}, target url: {TargetUrl}", result.Key, result.TargetUrl);
                return View("Result", resultUrl);
            }

            ViewBag.ValidationErrors = validationResult.Errors;

            logger.Information("Shortening request failed - validation error, target url: {TargetUrl}", model.TargetUrl);
            return View("Index");
        }

        [HttpGet("{key}", Name = RedirectActionName)]
        public async Task<IActionResult> Redirect(string key)
        {
            var result = await shortenerService.Get(key);
            if (result != null)
            {
                logger.Information("Requested redirection for the key {Key}, target url: {TargetUrl}", result.Key, result.TargetUrl);
                return RedirectPermanent(result.TargetUrl);
            }
                
            ViewBag.RedirectError = $"Shortened value '{key}' not found";

            logger.Information("Requested redirection for the key {key}, not found", key);
            return View("Index");
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            //paging should be applied
            var results = await shortenerService.GetAll();

            //DTO in this case is redundant, but it is not a good idea to return an entity class to view
            var dtos = results.Select(r => new ShorteningResultDTO
            {
                ShortenedUrl = linkGenerator.GetUriByRouteValues(HttpContext, routeName: RedirectActionName, values: new { key = r.Key })?.ToString(),
                TargetUrl = r.TargetUrl
            }).ToArray();

            var model = new ListResultsViewModel { Results = dtos };

            logger.Information($"Requested list");
            return View(model);
        }
    }
}