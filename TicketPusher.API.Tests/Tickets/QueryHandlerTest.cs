using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public abstract class QueryHandlerTest : IClassFixture<MapperFixture>
    {
        protected readonly MapperFixture _mapper;
        protected readonly DbContextOptions<TicketPusherContext> _dbContextOptions;

        public QueryHandlerTest(MapperFixture mapper)
        {
            _mapper = mapper;
            var connStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connStringBuilder.ToString());
            _dbContextOptions = new DbContextOptionsBuilder<TicketPusherContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new TicketPusherContext(_dbContextOptions);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }
    }
}