using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TicketPusher.API.Data;
using System.Linq;

namespace TicketPusher.API.Tests.Utils
{
    public class WebApplicationFixture : WebApplicationFactory<Startup>
    {
        // cf https://docs.microsoft.com/en-us/aspnet/core/test/integration-tests?view=aspnetcore-3.1
        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            // https configuration options here: https://docs.microsoft.com/en-us/aspnet/core/security/enforcing-ssl?view=aspnetcore-2.2&tabs=visual-studio
            builder.UseSetting("https_port", "5001");

            builder.ConfigureServices(services =>
            {
                var descriptor = services.SingleOrDefault(
                    d => d.ServiceType == 
                        typeof(DbContextOptions<TicketPusherContext>));

                if (descriptor != null) services.Remove(descriptor);

                // make sure to call .Open() !! cf https://github.com/dotnet/efcore/issues/5086
                var inMemorySqlite = new SqliteConnection("Data Source=:memory:");
                inMemorySqlite.Open();
                services.AddDbContext<TicketPusherContext>(options =>
                {
                    options.UseSqlite(inMemorySqlite);
                });

                var sp = services.BuildServiceProvider();

                using var scope = sp.CreateScope();
                var ticketPusherDb = scope.ServiceProvider
                    .GetRequiredService<TicketPusherContext>();
                ticketPusherDb.Database.EnsureCreated();
            });
        }
    }
}