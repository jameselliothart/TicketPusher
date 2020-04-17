using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TicketPusher.Domain.CompletedTickets
{
    public class CompletedDetails : ValueObject
    {
        public DateTime CompletionDate { get; }
        public string Resolution { get; }

        public CompletedDetails(DateTime completionDate, string resolution)
        {
            CompletionDate = completionDate;
            Resolution = resolution;
        }


        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return CompletionDate;
            yield return Resolution;
        }
    }
}