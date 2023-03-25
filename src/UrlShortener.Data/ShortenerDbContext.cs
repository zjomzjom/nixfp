using Microsoft.EntityFrameworkCore;
using System.Reflection;
using UrlShortener.Data.Entities;

namespace UrlShortener.Data
{
    public class ShortenerDbContext : DbContext
    {
        public ShortenerDbContext(DbContextOptions options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }

        public virtual DbSet<ShorteningResult> ShorteningResults { get; set; }
    }
}
