using AutoMapper;
using TicketPusher.API.Tickets;
using System.Reflection;

namespace TicketPusher.API.Tests.Utils
{
    public class MapperFixture
    {
        public IMapper Instance { get; }
        public MapperFixture()
        {
            var config = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(Assembly.GetAssembly(typeof(TicketMapper)));
            });
            Instance = config.CreateMapper();
        }
    }
}