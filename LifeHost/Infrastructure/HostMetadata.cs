using System;
using System.Collections.Generic;

namespace LifeHost.Infrastructure
{
    public class HostMetadata
    {
        public Guid Id;
        public IEnumerable<Task> TasksInProcess { get; set; }
    }

    public class Task
    {
        public Guid Id;
        public DateTime Start;
        public bool IsError;
        public bool Ready;
        public string Message;
        public List<bool> Array;
    }
}