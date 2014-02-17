using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.Common.Server.EngineActors
{
  public abstract class EngineActorBase
  {
    protected EngineActorBase(EngineManager xiEngine)
    {
      Engine = xiEngine;
    }

    public abstract void ActNextFrame(Data xiData);

    protected EngineManager Engine;
  }
}
