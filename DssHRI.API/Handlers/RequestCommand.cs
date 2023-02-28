using DssHRI.Domain.Models;

namespace DssHRI.API.Handlers
{
    public class RequestCommand
    {
        public string Topic { get; set; }
        public Person Person { get; set; }

        public RequestCommand() { }
    }
}
