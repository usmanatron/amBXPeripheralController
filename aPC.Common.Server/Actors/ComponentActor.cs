using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class ComponentActor<T> : IActor<ComponentSnapshot<T>> where T : IComponent
  {
    private IEngine engine;

    public ComponentActor(IEngine engine)
    {
      this.engine = engine;
    }

    public void ActNextFrame(eDirection direction, ComponentSnapshot<T> snapshot)
    {
      if (!snapshot.IsComponentNull)
      {
        engine.UpdateComponent(direction, snapshot.Item, snapshot.FadeTime);
      }
    }
  }
}