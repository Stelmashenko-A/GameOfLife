using LifeHost.Controllers;
using LifeHost.Models;

namespace LifeHost.Business.GameOfLife
{
    public interface IGameOfLife
    {
        void Process(RequestForProcessing request);
    }
}