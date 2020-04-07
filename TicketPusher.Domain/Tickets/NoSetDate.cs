using System;

namespace TicketPusher.Domain.Tickets
{
    public class NoSetDate
    {
        private static DateTime instance = DateTime.MaxValue;

        private NoSetDate()
        {
        }

        public static DateTime Instance => instance;
    }
}