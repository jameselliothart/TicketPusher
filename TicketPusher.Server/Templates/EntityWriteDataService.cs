using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using TicketPusher.DataTransfer;

namespace TicketPusher.Server.Templates
{
    public abstract class EntityWriteDataService<TDto, TCreateDto> : EntityDataService, IEntityWriteDataService<TDto, TCreateDto>
    {

        public EntityWriteDataService(HttpClient httpClient, string apiBaseUri) : base(httpClient, apiBaseUri)
        {
        }

        public async Task<EnvelopeDto<TDto>> CreateEntityAsync(TCreateDto project)
        {
            var data = await HttpClient.PostJsonAsync<EnvelopeDto<TDto>>(ApiBaseUri, project);
            return data;
        }
    }
}