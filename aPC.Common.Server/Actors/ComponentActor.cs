using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public abstract class ComponentActor<T> : ActorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentActor(IEngine engine)
      : base(engine)
    {
    }
  }
}