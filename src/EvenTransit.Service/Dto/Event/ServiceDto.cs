using System.Collections.Generic;

namespace EvenTransit.Service.Dto.Event
{
    public class ServiceDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Timeout { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}