using aPC.Common.Entities;
using aPC.Server.Engine;
using aPC.Server.Snapshots;

namespace aPC.Server.EngineActors
{
  public abstract class ComponentActor<T> : EngineActorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentActor(IEngine xiEngine)
      : base (xiEngine)
    {
    }
  }
}
