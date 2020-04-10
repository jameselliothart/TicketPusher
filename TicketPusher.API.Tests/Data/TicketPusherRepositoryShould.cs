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
        private readonly string _connectionForTests;
        private readonly string _connectionForCleanup;
        public TicketPusherContext context { get; private set; }
        public TicketPusherRepository repository { get; private set; }
        public DatabaseFixture()
        {
            _databaseName = Guid.NewGuid().ToString();

            _connectionForTests = $"host=localhost;database={_databaseName};user id=postgres;password=docker;";
            _connectionForCleanup = $"host=localhost;database=postgres;user id=postgres;password=docker;";

            var configBuilder = new ConfigurationBuilder();
            var settings = new Dictionary<string, string>
                {
                    {"ConnectionStrings:TicketPusherDb", $"{_connectionForTests}"}
                };
            configBuilder.AddInMemoryCollection(settings);
            IConfiguration config = configBuilder.Build();

            var options = new DbContextOptionsBuilder<TicketPusherContext>()
                .UseNpgsql(_connectionForTests)
                .Options;

            context = new TicketPusherContext(options);
            context.Database.Migrate();

            repository = new TicketPusherRepository(context, config);
        }

        public void Dispose()
        {
            // need to switch the context to a new database to drop the temp one
            var options = new DbContextOptionsBuilder<TicketPusherContext>()
                .UseNpgsql(_connectionForCleanup)
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
            var ticket = CreateTestTicket();

            // Act
            _db.repository.CreateTicket(ticket);
            _db.repository.SaveChanges();

            // Assert
            var ticketFromRepo = _db.context.Tickets.FirstOrDefault(t => t.Id == ticket.Id);
            Assert.Equal(ticket, ticketFromRepo);
        }

        [Fact]
        public void RetrieveATicket()
        {
            // Arrange
            var ticket = CreateTestTicket();
            _db.context.Tickets.Add(ticket);
            _db.context.SaveChanges();

            // Act
            TicketDto ticketDto = _db.repository.GetTicket(ticket.Id);

            // Assert
            Assert.Equal(ticket.Id, ticketDto.Id);
        }

        private Ticket CreateTestTicket()
        {
            var ticketId = Guid.NewGuid();
            return new Ticket(ticketId, "owner", "desc", DateTime.Now, NoSetDate.Instance);
        }

    }

}