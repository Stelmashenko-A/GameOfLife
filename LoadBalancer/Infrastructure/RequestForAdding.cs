using System;

namespace LoadBalancer.Infrastructure
{
    public class RequestForAdding
    {
        public Guid RequestId { get; set; }
        public string Host { get; set; }
        public DateTime TimeOfGet { get; set; }
        public string Key { get; set; }
    }
}