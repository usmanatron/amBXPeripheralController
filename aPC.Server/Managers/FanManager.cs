using aPC.Common.Entities;
using System.Linq;
using System;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshot;
using System.Collections.Generic;
using aPC.Server.EngineActors;
using aPC.Common;

namespace aPC.Server.Managers
{
  class FanManager : ComponentManager
  {
    public FanManager(eDirection xiDirection, FanActor xiActor, Action xiEventCallback)
      : base (xiDirection, xiActor, xiEventCallback)
    {
      Direction = xiDirection;
      SetupNewScene(CurrentScene);
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

    public override SnapshotBase GetNextSnapshot()
    {
      var lFrame = GetNextFrame();
      var lFan = GetFan(Direction, lFrame.Fans);

      return lFan == null
        ? new ComponentSnapshot(lFrame.Length)
        : new ComponentSnapshot(lFan, lFrame.Fans.FadeTime, lFrame.Length);
    }

    private Fan GetFan(eDirection xiDirection, FanSection xiFans)
    {
      return xiFans.GetComponentValueInDirection(xiDirection);
    }

    public override eComponentType ComponentType()
    {
      return eComponentType.Fan;
    }
  }
}
