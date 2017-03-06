using System;
using System.Collections.Generic;

namespace LoadBalancer.Business.RouteTableStorage
{
    public class Route
    {
        public Guid RouteId { get; set; }
        public string Host { get; set; }
        public IList<Guid> CurrentTask { get; set; }
        public DateTime LastConnection { get; set; }
    }
}