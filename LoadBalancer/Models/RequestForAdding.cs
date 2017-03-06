using System;

namespace LoadBalancer.Business
{
    public class RequestForAdding
    {
        public Guid RequestId { get; set; }
        public string Host { get; set; }
        public DateTime TimeOfGet { get; set; }
        public string Key { get; set; }
    }
}