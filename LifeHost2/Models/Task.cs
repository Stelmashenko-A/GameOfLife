using System;
using System.Collections.Generic;

namespace LifeHost.Models
{
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