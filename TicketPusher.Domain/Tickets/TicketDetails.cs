using System;
using System.Collections.Generic;
using CSharpFunctionalExtensions;

namespace TicketPusher.Domain.Tickets
{
    public class TicketDetails : ValueObject
    {
        public string Description { get; }
        public DateTime SubmitDate { get; }
        public DateTime DueDate { get; }

        public TicketDetails(string description, DateTime submitDate, DateTime dueDate)
        {
            Description = description ?? throw new ArgumentNullException(nameof(description));
            SubmitDate = submitDate;
            DueDate = dueDate;
        }

        protected override IEnumerable<object> GetEqualityComponents()
        {
            yield return Description;
            yield return SubmitDate;
            yield return DueDate;
        }
    }
}