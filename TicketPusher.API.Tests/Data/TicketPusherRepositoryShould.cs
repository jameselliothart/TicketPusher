using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TicketPusher.API.Data;
using TicketPusher.Domain.Tickets;
using Xunit;

namespace TicketPusher.API.Tests.Data
{
    public class DatabaseFixture : IDisposable
    {
        private readonly string _databaseName;
        private readonly string connectionForTests;
        private readonly string connectionForCleanup;
        public TicketPusherContext context { get; private set; }
        public TicketPusherRepository repository { get; private set; }
        public DatabaseFixture()
        {
            _databaseName = Guid.NewGuid().ToString();

            connectionForTests = $"host=localhost;database={_databaseName};user id=postgres;password=docker;";
            connectionForCleanup = $"host=localhost;database=postgres;user id=postgres;password=docker;";

            var options = new DbContextOptionsBuilder<TicketPusherContext>()
                .UseNpgsql(connectionForTests)
                .Options;

            context = new TicketPusherContext(options);
            context.Database.Migrate();

            repository = new TicketPusherRepository(context);
        }
        // zspnlcyp / c2PKfjpC

        public void Dispose()
        {
            // need to switch the context to a new database to drop the temp one
            var options = new DbContextOptionsBuilder<TicketPusherContext>()
                .UseNpgsql(connectionForCleanup)
                .Options;
            context = new TicketPusherContext(options);

            string dropDb = $@"
            REVOKE CONNECT ON DATABASE ""{_databaseName}"" FROM public;
            SELECT pg_terminate_backend(pg_stat_activity.pid)
            FROM pg_stat_activity
            WHERE pg_stat_activity.datname = '{_databaseName}';
            DROP DATABASE ""{_databaseName}"";";
            context.Database.ExecuteSqlRaw(dropDb);
        }
    }

    public class TicketPusherRepositoryShould : IClassFixture<DatabaseFixture>
    {
        private readonly DatabaseFixture _db;

        public TicketPusherRepositoryShould(DatabaseFixture db)
        {
            _db = db;
        }

        [Fact]
        public void CreateATicket()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var ticket = new Ticket(ticketId, "owner", "desc", DateTime.Now, NoSetDate.Instance);

            // Act
            _db.repository.CreateTicket(ticket);
            _db.repository.SaveChanges();

            // Assert
            var ticketFromRepo = _db.context.Tickets.FirstOrDefault(t => t.Id == ticketId);
            Assert.Equal(ticket, ticketFromRepo);
        }

    }

}