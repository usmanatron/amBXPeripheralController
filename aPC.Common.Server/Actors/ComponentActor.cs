using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class ComponentActor : IActor<ComponentSnapshot>
  {
    private IEngine engine;
    public eComponentType ComponentType;

    public ComponentActor(eComponentType componentType, IEngine engine)
    {
      this.ComponentType = componentType;
      this.engine = engine;
    }

    public void ActNextFrame(eDirection direction, ComponentSnapshot snapshot)
    {
      if (!snapshot.IsComponentNull)
      {
        engine.UpdateComponent(direction, snapshot.Item, snapshot.FadeTime);
      }
    }
  }
}