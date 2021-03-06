using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TicketPusher.API.Utils
{
    public class Error : ValueObject
    {
        public string Code { get; }
        public string Message { get; }

        internal Error(string code, string message)
        {
            Code = code;
            Message = message;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Code;
        }
    }
}