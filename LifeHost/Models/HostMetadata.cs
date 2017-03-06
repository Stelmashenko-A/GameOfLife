using System;
using System.Collections.Generic;

namespace LifeHost.Models
{
    public class HostMetadata
    {
        public Guid Id;
        public IEnumerable<Task> TasksInProcess { get; set; }
    }
}