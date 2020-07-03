using System;

namespace TicketPusher.DataTransfer
{
    public class EnvelopeDto<T>
    {
        public T Result { get; set; }
        public string ErrorMessage { get; set; }
        public DateTime TimeGenerated { get; set; }
    }
}