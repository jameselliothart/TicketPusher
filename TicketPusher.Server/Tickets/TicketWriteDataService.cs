using System;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.DataTransfer;
using TicketPusher.DataTransfer.CompletedTickets;
using TicketPusher.DataTransfer.Tickets;
using TicketPusher.Server.Templates;

namespace TicketPusher.Server.Tickets
{
    public class TicketWriteDataService : EntityWriteDataService<TicketDto, SubmitTicketDto>, ITicketWriteDataService
    {
        public TicketWriteDataService(HttpClient httpClient) : base(httpClient, "tickets")
        {
        }

        public async Task<EnvelopeDto<CompletedTicketDto>> CloseTicketAsync(Guid ticketId, CloseTicketDto dto)
        {
            var data = await HttpClient.PostJsonAsync<EnvelopeDto<CompletedTicketDto>>(
                $"{ApiBaseUri}/{ticketId}/close", dto);
            return data;
        }
    }
}