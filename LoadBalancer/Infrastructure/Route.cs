using System;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Security.Policy;

namespace LoadBalancer.Infrastructure
{
    public class Route
    {
        public Guid RouteId { get; set; }
        public string Host { get; set; }
        public Guid CurrentTast { get; set; }
        public DateTime LastConnection { get; set; }
    }
}