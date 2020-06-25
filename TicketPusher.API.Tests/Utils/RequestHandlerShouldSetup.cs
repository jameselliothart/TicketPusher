using System;
using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.Utils
{
    public abstract class RequestHandlerShouldSetup : IClassFixture<MapperFixture>
    {
        protected static MapperFixture _mapper;
        protected readonly DbContextOptions<TicketPusherContext> _dbContextOptions;

        public RequestHandlerShouldSetup()
        {
            var connStringBuilder = new SqliteConnectionStringBuilder { DataSource = ":memory:" };
            var connection = new SqliteConnection(connStringBuilder.ToString());
            _dbContextOptions = new DbContextOptionsBuilder<TicketPusherContext>()
                .UseSqlite(connection)
                .Options;

            using var context = new TicketPusherContext(_dbContextOptions);
            context.Database.OpenConnection();
            context.Database.EnsureCreated();
        }

        public RequestHandlerShouldSetup(MapperFixture mapper) : this()
        {
            _mapper = mapper;
        }

        public void ActWithRepository(Action<TicketPusherRepository> act)
        {
            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                var repo = new TicketPusherRepository(context);
                act(repo);
            }
        }

        public void ActWithContext(Action<TicketPusherContext> act)
        {
            using (var context = new TicketPusherContext(_dbContextOptions))
            {
                act(context);
            }
        }

    }
}