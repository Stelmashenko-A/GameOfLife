using System.Collections.Generic;
using LoadBalancer.Business.RouteTableStorage;

namespace LoadBalancer.Models
{
    public class Hosts
    {
        public Hosts(IList<Route> items)
        {
            Items = items;
        }

        public IList<Route> Items { get; private set; }
    }
}