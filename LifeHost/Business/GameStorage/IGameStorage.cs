using System;

namespace LifeHost.Business.GameStorage
{
    public interface IGameStorage : IDisposable
    {
        void Save(Part part);
        Part Get(Guid taskId, int partId);
        void RemoveAllParts(Guid taskId);
    }
}
