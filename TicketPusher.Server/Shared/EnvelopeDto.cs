using System;

namespace TicketPusher.Server.Shared
{
    public class EnvelopeDto<T>
    {
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime TimeGenerated { get; set; }
    }
}