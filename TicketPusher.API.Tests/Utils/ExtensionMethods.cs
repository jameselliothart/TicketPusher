using System.Net.Http;
using System.Text;
using Newtonsoft.Json;

namespace TicketPusher.API.Tests.Utils
{
    public static class ExtensionMethods
    {
        public static StringContent JsonContent(this object obj)
        {
            return new StringContent(JsonConvert.SerializeObject(obj), Encoding.UTF8, "application/json");
        }
    }
}