using System;
using System.Collections.Generic;

namespace LoadBalancer.Business.RouteTableStorage
{
    public class Route
    {
        public Guid RouteId { get; set; }
        public string Host { get; set; }
        public IList<GameTask> CurrentTasks { get; set; } = new List<GameTask>();
        public DateTime LastConnection { get; set; }
    }

    public class GameTask
    {
        public Guid TaskId { get; set; }
        public int PartId { get; set; }
    }
}