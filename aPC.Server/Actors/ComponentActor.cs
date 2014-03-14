using aPC.Common.Entities;
using aPC.Server.Engine;
using aPC.Server.Snapshots;

namespace aPC.Server.Actors
{
  public abstract class ComponentActor<T> : ActorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentActor(IEngine xiEngine)
      : base (xiEngine)
    {
    }
  }
}
