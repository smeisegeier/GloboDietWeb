using GloboDiet.Services;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GloboDiet.Extensions
{
    public static class HostExtensions
    {
        public static IHost SeedDb(this IHost host)
        {
            // host -> scope -> serviceProvider -> context
            var preContext = host
                .Services.CreateScope()
                .ServiceProvider.GetRequiredService<GloboDietDbContext>();
            preContext.SeedDb();
            return host;
        }
    }
}
