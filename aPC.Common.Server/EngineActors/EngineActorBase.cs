using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.Common.Server.EngineActors
{
  public abstract class EngineActorBase
  {
    protected EngineActorBase(EngineManager xiEngine, ManagerBase xiManager)
    {
      Engine = xiEngine;
      Manager = xiManager;
    }

    public abstract void ActNextFrame(Data xiData);

    protected ManagerBase Manager;
    protected EngineManager Engine;
  }
}
