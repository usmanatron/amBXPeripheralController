using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.SceneHandlers
{
  class RumbleHandler : ComponentHandler<Rumble>
  {
    public RumbleHandler(Action xiEventcomplete) : base(xiEventcomplete)
    { 
    }

    // A scene is applicable if there is at least one non-null rumble in the right direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lRumbles = xiFrames
        .Select(frame => frame.Rumbles)
        .Where(section => section != null);

      return lRumbles.Any(rumble => rumble != null);
    }

    public override ComponentSnapshot<Rumble> GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();

      Rumble lRumble = lFrame.Rumbles == null
        ? null
        : lFrame.Rumbles.Rumble;

      return lRumble == null
        ? new ComponentSnapshot<Rumble>(lFrame.Length)
        : new ComponentSnapshot<Rumble>(lRumble, lFrame.Rumbles.FadeTime, lFrame.Length);
    }
  }
}
