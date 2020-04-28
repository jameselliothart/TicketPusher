using System.Collections.Generic;
using Microsoft.AspNetCore.Components;

namespace TicketPusher.Server.Templates
{
    public partial class TableTemplate<TItem>
    {
        [Parameter]
        public RenderFragment TableHeader { get; set; }

        [Parameter]
        public RenderFragment<TItem> RowTemplate { get; set; }

        [Parameter]
        public IEnumerable<TItem> Items { get; set; }
    }
}