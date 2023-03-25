using Microsoft.EntityFrameworkCore;
using Serilog;
using UrlShortener.Data;
using UrlShortener.WebApplication.Configuration;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddCoreComponents();
builder.Services.AddSerilog(builder.Configuration);
builder.Services.AddDbContext<ShortenerDbContext>(opt =>
{
    opt.UseInMemoryDatabase("db");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
