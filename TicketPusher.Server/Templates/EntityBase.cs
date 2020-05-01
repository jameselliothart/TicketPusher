using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace TicketPusher.Server.Templates
{
    public abstract class EntityBase<T, TService> : ComponentBase
    {
        [Inject]
        protected TService EntityDataService { get; set; }

        public List<T> Entities;

        protected override async Task OnInitializedAsync()
        {
            await RefreshData();
        }

        protected async Task RefreshData()
        {
            Entities = await RetrieveData();
        }

        protected abstract Task<List<T>> RetrieveData();
    }
}