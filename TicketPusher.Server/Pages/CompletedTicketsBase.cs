using Microsoft.AspNetCore.Components;

namespace TicketPusher.Server.Pages
{
	
	public class CompletedTicketsBase : ComponentBase
	{		
		public int currentCount = 0;

		public void IncrementCount()
		{
			currentCount++;
		}
	}
}