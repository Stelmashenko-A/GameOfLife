using System;
using System.Collections.Generic;

namespace LifeHost.Storage
{
    public class Part
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public int PartNumber { get; set; }
        public IEnumerable<string> Steps { get; set; }
    }
}