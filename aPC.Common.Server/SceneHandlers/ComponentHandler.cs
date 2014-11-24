using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class ComponentHandler : SceneHandlerBase<ComponentSnapshot>
  {
    private eComponentType componentType;

    public ComponentHandler(eComponentType componentType, amBXScene scene, Action eventComplete)
      : base(scene, eventComplete)
    {
      this.componentType = componentType;
    }

    public override ComponentSnapshot GetNextSnapshot(eDirection direction)
    {
      var frame = GetNextFrame();
      var componentSection = GetSection(frame);
      var component = componentSection.GetComponentValueInDirection(direction);

      return component == null
        ? new ComponentSnapshot(frame.Length)
        : new ComponentSnapshot(component, frame.Length);
    }

    private IComponentSection GetSection(Frame frame)
    {
      switch (componentType)
      {
        case eComponentType.Light:
          return frame.Lights;
        case eComponentType.Fan:
          return frame.Fans;
        case eComponentType.Rumble:
          return frame.Rumbles;
        default:
          throw new InvalidOperationException("Unexpected Component Type!");
      }
    }
  }
}