using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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

            var configBuilder = new ConfigurationBuilder();
            var settings = new Dictionary<string, string>
                {
                    {"ConnectionStrings:TicketPusherDb", $"{connectionForTests}"}
                };
            configBuilder.AddInMemoryCollection(settings);
            IConfiguration config = configBuilder.Build();

            var options = new DbContextOptionsBuilder<TicketPusherContext>()
                .UseNpgsql(connectionForTests)
                .Options;

            context = new TicketPusherContext(options);
            context.Database.Migrate();

            repository = new TicketPusherRepository(context, config);
        }

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

        [Fact]
        public void RetrieveATicket()
        {
            // Arrange
            var ticketId = Guid.NewGuid();
            var ticket = new Ticket(ticketId, "owner", "desc", DateTime.Now, NoSetDate.Instance);
            _db.context.Tickets.Add(ticket);
            _db.context.SaveChanges();

            // Act
            TicketDto ticketDto = _db.repository.GetTicket(ticketId);

            // Assert
            Assert.Equal(ticketId, ticketDto.Id);
        }

    }

}