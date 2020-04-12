using AutoMapper;
using TicketPusher.API.Tickets;

namespace TicketPusher.API.Tests.Utils
{
    public class MapperFixture
    {
        public IMapper Instance { get; }
        public MapperFixture()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new TicketMapper());
            });
            Instance = config.CreateMapper();
        }
    }
}