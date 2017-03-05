using System;

namespace LoadBalancer.Models
{
    public class RequestForProcessing
    {
        public Guid Id { get; set; }
        public string Field { get; set; }
        public int Steps { get; set; }
        public int Parts { get; set; }
    }
}