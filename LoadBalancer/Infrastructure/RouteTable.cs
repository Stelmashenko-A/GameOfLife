using System.Collections.Generic;

namespace LoadBalancer.Infrastructure
{
    public class RouteTable
    {
        public string Id = "RouteTable";
        public IList<Route> Routes { get; set; } = new List<Route>();
        public IList<RequestForAdding> RequestForAddings { get; set; } = new List<RequestForAdding>();
    }
}