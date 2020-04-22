using System;

namespace TicketPusher.API.Utils
{
    public static class Errors
    {
        public static class Ticket
        {
        }

        public static class General
        {
            public static Error NotFound(string entityName, Guid id) =>
                new Error("record.not.found", $"'{entityName}' not found for Id '{id}'");

        }
    }
}