using aPC.Common.Server.Snapshots;
using aPC.Common.Entities;

namespace aPC.Common.Server.EngineActors
{
  public abstract class ComponentActor<T> : EngineActorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentActor(IEngine xiEngine)
      : base (xiEngine)
    {
    }
  }
}
