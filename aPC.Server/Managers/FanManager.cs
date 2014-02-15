using amBXLib;
using aPC.Common.Entities;
using System.Linq;
using System;
using aPC.Common.Server.Managers;
using System.Collections.Generic;
using aPC.Common;

namespace aPC.Server.Managers
{
  class FanManager : ManagerBase
  {
    public FanManager(eDirection xiDirection)
      : this(xiDirection, null)
    {
    }

    public FanManager(eDirection xiDirection, Action xiEventCallback)
      : base(xiEventCallback)
    {
      mDirection = xiDirection;
      SetupNewScene(CurrentScene);
    }

    // A scene is applicable if there is at least one non-null fan in a "somewhat" correct direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFans = xiFrames
        .Select(frame => frame.Fans)
        .Where(section => section != null)
        .Select(section => GetFan(mDirection, section));

      return lFans.Any(fan => fan != null);
    }

    public override Data GetNext()
    {
      var lFrame = GetNextFrame();
      var lFan = GetFan(mDirection, lFrame.Fans);

      return lFan == null
        ? new ComponentData(lFrame.Length)
        : new ComponentData(lFan, lFrame.Fans.FadeTime, lFrame.Length);
    }

    private Fan GetFan(eDirection xiDirection, FanSection xiFans)
    {
      return (Fan)GetComponentInDirection(xiDirection, xiFans);
    }

    readonly eDirection mDirection;
  }
}
