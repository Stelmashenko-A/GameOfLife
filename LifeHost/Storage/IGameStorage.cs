using System;
using System.Collections.Generic;

namespace LifeHost.Storage
{
    public interface IGameStorage
    {
        void Save(Part part);
        Part Get(Guid taskId, int partId);
        void RemoveAllParts(Guid taskId);
    }

    public class Part
    {
        public Guid Id { get; set; }
        public Guid TaskId { get; set; }
        public int PartNumber { get; set; }
        public IEnumerable<string> Steps { get; set; }
    }
}
