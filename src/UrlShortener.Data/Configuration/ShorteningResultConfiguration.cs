using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using UrlShortener.Data.Entities;

namespace UrlShortener.Data.Configuration
{
    internal class ShorteningResultConfiguration : IEntityTypeConfiguration<ShorteningResult>
    {
        public void Configure(EntityTypeBuilder<ShorteningResult> builder)
        {
            builder.HasKey(e => e.Key);

            builder.HasIndex(e => e.Key).IsUnique();

            builder.Property(e => e.Key).IsRequired();
            builder.Property(e => e.TargetUrl).IsRequired();
        }
    }
}
