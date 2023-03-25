using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UrlShortener.Data;

namespace UrlShortener.Domain.Tests
{
    public static class TestsUtils
    {
        public static ShortenerDbContext CreateInMemoryDbContext()
        {
            var optionsBuilder =  new DbContextOptionsBuilder();
            optionsBuilder.UseInMemoryDatabase("db");
            return new ShortenerDbContext(optionsBuilder.Options);
        }
    }
}
