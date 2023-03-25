# UrlShortener
Simple MVC application. All actions are defined in the Home controller. Generating a unique value based on the GUID.

## How to start?
EF in memory db is used, so there is nothing that should be done as a prerequisite.

## Key assumptions 
- I used EntityFramework in memory. Code-first approach. I did not add a layer of repositories.
- Generating a unique value based on the GUID. I know that a collision can happen. In ShortenerService.Generate(), there should be added logic to retry with the new key value when the base returns a unique index violation.
- To ensure adequate performance, I added the use of memory cache as a decorator on the ShortenerService. I know that the lack of a limit on the size of this cache can lead to redundant memory usage and it should be added.
- I used Razor Pages, so I no longer added integration tests using the test server.
- The regex for the url validation was taken from the Internet.
- I know that returning all values from a DbSet is not a good idea. There should be a pagination added to the /list resource.
- I used Serilog to capture the logs in a file. The configuration can be found in the appsettings.json file.

## Future Ideas
- IMemoryCache size limit.
- Paging in the /list resource.
- Retry logic when the unique index violation occurs (collision on keys).
- Some integration tests.
- Some length limit of the given URL.

## Task Description 
>Build a URL shortening service like TinyURL. This service will provide short aliases redirecting to long URLs.
### Why do we use Url shortening?
URL shortening is used to create shorter aliases for long URLs. We call these shortened aliases “short links.” Users are redirected to the original URL when they hit these short links. Short links save a lot of space when displayed, printed, messaged, or tweeted. Additionally, users are less likely to mistype shorter URLs.

For example, if we shorten the following URL: `https://www.some-website.io/realy/long-url-with-some-random-string/m2ygV4E81AR`

We would get something like this: `https://short.url/xer23`

URL shortening is used to optimize links across devices, track individual links to analyze audience, measure ad campaigns’ performance, or hide affiliated original URLs.

### URL shortening application should have:
 - A page where a new URL can be entered and a shortened link is generated. You can use Home page.
 - A page that will show a list of all the shortened URL’s.
### Functional Requirements:
- Given a URL, our service should generate a shorter and unique alias of it. This is called a short link. This link should be short enough to be easily copied and pasted into applications.
- When users access a short link, our service should redirect them to the original link.
- Application should store logs information about requests.
### Non-Functional Requirements:
- URL redirection should happen in real-time with minimal latency.
- Please add small project description to Readme.md file.
### During implementation please pay attention to:
- Application is runnable out of box. If some setup is needed please provide details on ReadMe file.
- Project structure and code smells.
- Design Principles and application testability.
- Main focus should be on backend functionality, not UI.
- Input parameter validation.
- Please, don't use any library or service that implements the core functionality of this test.
### Other recommendation:
- You may change UI technology to any other you are most familiar with.
- You can use InMemory data storage.
- You can use the Internet.
# May the force be with you {username}!
