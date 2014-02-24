using aPC.Common.Server.Snapshots;
using aPC.Common.Entities;

namespace aPC.Common.Server.EngineActors
{
  public abstract class ComponentActor<T> : EngineActorBase<ComponentSnapshot<T>> where T : IComponent
  {
    protected ComponentActor(eDirection xiDirection, EngineManager xiEngine)
      : base (xiEngine)
    {
      Direction = xiDirection;
    }

    protected readonly eDirection Direction;
  }
}
