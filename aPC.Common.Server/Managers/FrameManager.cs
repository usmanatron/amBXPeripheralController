using aPC.Common.Entities;
using aPC.Common.Server.EngineActors;
using System.Linq;
using System;
using System.Collections.Generic;

namespace aPC.Common.Server.Managers
{
  public class FrameManager : ManagerBase
  {
    public FrameManager(FrameActor xiActor) 
      : this(xiActor, null)
    {
    }

    public FrameManager(EngineActorBase xiActor, Action xiEventComplete)
      : base(xiActor, xiEventComplete)
    {
      SetupNewScene(CurrentScene);
    }

    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFrames = xiFrames
        .Where(frame => frame.Lights  != null || 
                        frame.Fans    != null || 
                        frame.Rumbles != null);

      return lFrames.Any(frame => frame != null);
    }

    public override SnapshotBase GetNextData()
    {
      var lFrame = GetNextFrame();
      return new FrameSnapshot(lFrame, 0);
    }
  }
}
