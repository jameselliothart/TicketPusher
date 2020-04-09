using System;
using System.Collections.Generic;
using System.Linq;
using Dapper;
using Microsoft.Extensions.Configuration;
using Npgsql;
using TicketPusher.Domain.Tickets;

namespace TicketPusher.API.Data
{
    public class TicketPusherRepository : IDisposable
    {
        private readonly TicketPusherContext _context;
        private string _readConnectionString;
        public NpgsqlConnection _reads => new NpgsqlConnection(_readConnectionString);

        public TicketPusherRepository(TicketPusherContext context, IConfiguration configuration)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _readConnectionString = configuration.GetConnectionString("TicketPusherDb");
        }

        public void CreateTicket(Ticket ticket)
        {
            if (ticket.Id == Guid.Empty) 
                throw new ArgumentException($"Ticket Id {ticket.Id} cannot be empty");
            if (ticket == null)
                throw new ArgumentNullException(nameof(ticket));

            _context.Tickets.Add(ticket);
        }

        public TicketDto GetTicket(Guid ticketId)
        {
            string sql = @"
                SELECT ""TicketId"" as Id, ""Owner"", ""Description"", ""SubmitDate"", ""DueDate""
                FROM ""Ticket""
                WHERE ""TicketId"" = @TicketId";

            using var connection = _reads;
            TicketDto ticket = connection.Query<TicketDto>(sql,
                new { TicketId = ticketId })
                .FirstOrDefault();

            return ticket;
        }

        public List<TicketDto> GetAllTickets()
        {
            string sql = @"
                SELECT ""TicketId"" as Id, ""Owner"", ""Description"", ""SubmitDate"", ""DueDate""
                FROM ""Ticket""";

            using var connection = _reads;
            List<TicketDto> tickets = connection.Query<TicketDto>(sql).ToList();

            return tickets;
        }

        public void RemoveTicket(Ticket ticket)
        {
            _context.Tickets.Remove(ticket);
        }

        public int SaveChanges()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context?.Dispose();
            GC.SuppressFinalize(this);
        }
    }
}