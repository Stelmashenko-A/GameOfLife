using System;

namespace LifeHost.Storage
{
    public interface IGameStorage : IDisposable
    {
        void Save(Part part);
        Part Get(Guid taskId, int partId);
        void RemoveAllParts(Guid taskId);
    }
}
