using System;

namespace TicketPusher.Server.Utils
{
    public static class GuidExtensions
    {
        public static string ToShortGuid(this Guid guid) => guid.ToString().Split("-")[0];
    }
}