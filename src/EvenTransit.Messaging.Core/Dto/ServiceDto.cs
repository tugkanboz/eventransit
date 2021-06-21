using System.Collections.Generic;

namespace EvenTransit.Messaging.Core.Dto
{
    public class ServiceDto
    {
        public string Name { get; set; }
        public string Url { get; set; }
        public int Timeout { get; set; }
        public string Body { get; set; }
        public Dictionary<string, string> Headers { get; set; }
    }
}