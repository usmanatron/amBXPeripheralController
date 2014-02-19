using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Common.Server.Snapshots;
using aPC.Common;
using System.Linq;
using System;
using System.Collections.Generic;
using aPC.Server.EngineActors;

namespace aPC.Server.Managers
{
  class RumbleManager : ComponentManager
  {
    public RumbleManager(eDirection xiDirection, RumbleActor xiActor, Action xiEventCallback) 
      : base(xiDirection, xiActor, xiEventCallback)
    {
      Direction = xiDirection;
      SetupNewScene(CurrentScene);
    }

    // A scene is applicable if there is at least one non-null rumble in the right direction defined.
    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lRumbles = xiFrames
        .Select(frame => frame.Rumbles)
        .Where(section => section != null);

      return lRumbles.Any(rumble => rumble != null);
    }

    public override SnapshotBase GetNextSnapshot()
    {
      var lFrame = GetNextFrame();

      Rumble lRumble = lFrame.Rumbles == null
        ? null
        : lFrame.Rumbles.Rumble;

      return lRumble == null
        ? new ComponentSnapshot<Rumble>(lFrame.Length)
        : new ComponentSnapshot<Rumble>(lRumble, lFrame.Rumbles.FadeTime, lFrame.Length);
    }

    public override eComponentType ComponentType()
    {
      return eComponentType.Rumble;
    }
  }
}
