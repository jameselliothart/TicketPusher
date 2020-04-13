using TicketPusher.API.Data;
using TicketPusher.API.Tests.Utils;
using Xunit;

namespace TicketPusher.API.Tests.Tickets
{
    public abstract class QueryHandlerTest : IClassFixture<MapperFixture>
    {
        protected readonly MapperFixture _mapper;
        protected readonly ITicketPusherRepository _repository;

        public QueryHandlerTest(MapperFixture mapper)
        {
            _mapper = mapper;
            _repository = new InMemoryRepository();
        }
    }
}