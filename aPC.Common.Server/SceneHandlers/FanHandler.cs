using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class FanHandler : ComponentHandler<Fan>
  {
    public FanHandler(amBXScene scene, Action eventComplete)
      : base(scene, eventComplete)
    {
    }

    public override ComponentSnapshot<Fan> GetNextSnapshot(eDirection direction)
    {
      var frame = GetNextFrame();
      var fan = GetFan(direction, frame.Fans);

      return fan == null
        ? new ComponentSnapshot<Fan>(frame.Length)
        : new ComponentSnapshot<Fan>(fan, frame.Fans.FadeTime, frame.Length);
    }

    private Fan GetFan(eDirection direction, FanSection fans)
    {
      return fans.GetComponentValueInDirection(direction);
    }
  }
}