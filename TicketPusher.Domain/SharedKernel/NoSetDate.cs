using System;

namespace TicketPusher.Domain.SharedKernel
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