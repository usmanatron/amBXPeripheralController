using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.SceneHandlers
{
  class FanHandler : ComponentHandler<Fan>
  {
    public FanHandler(Action xiEventcomplete) : base(xiEventcomplete)
    { 
    }
    // A scene is applicable if there is at least one non-null fan in a "somewhat" correct direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFans = xiFrames
        .Select(frame => frame.Fans)
        .Where(section => section != null)
        .Select(section => GetFan(Direction, section));

      return lFans.Any(fan => fan != null);
    }

    public override ComponentSnapshot<Fan> GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();
      var lFan = GetFan(xiDirection, lFrame.Fans);

      return lFan == null
        ? new ComponentSnapshot<Fan>(lFrame.Length)
        : new ComponentSnapshot<Fan>(lFan, lFrame.Fans.FadeTime, lFrame.Length);
    }

    private Fan GetFan(eDirection xiDirection, FanSection xiFans)
    {
      return xiFans.GetComponentValueInDirection(xiDirection);
    }
  }
}
