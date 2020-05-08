using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace TicketPusher.API.Data
{
    public static class MigrateDb
    {
        public static void Migrate(this IApplicationBuilder app)
        {
            var context = app.ApplicationServices.GetService<TicketPusherContext>();

            context.Database.Migrate();
        }
    }
}