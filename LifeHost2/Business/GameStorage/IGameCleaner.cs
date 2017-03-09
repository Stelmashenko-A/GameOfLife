using System;

namespace LifeHost.Business.GameStorage
{
    public interface IGameCleaner : IDisposable
    {
        void Clean();
    }
}