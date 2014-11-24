using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class RumbleHandler : ComponentHandler
  {
    public RumbleHandler(amBXScene scene, Action eventComplete)
      : base(scene, eventComplete)
    {
    }

    public override ComponentSnapshot GetNextSnapshot(eDirection direction)
    {
      var frame = GetNextFrame();

      Rumble rumble = frame.Rumbles == null
        ? null
        : frame.Rumbles.Rumble;

      return rumble == null
        ? new ComponentSnapshot(frame.Length)
        : new ComponentSnapshot(rumble, frame.Rumbles.FadeTime, frame.Length);
    }
  }
}