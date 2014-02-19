using System.Collections.Generic;
using System.Linq;
using aPC.Common.Server.Snapshots;
using aPC.Common.Entities;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class FrameHandler : SceneHandlerBase<FrameSnapshot>
  {
    public FrameHandler()
      : this(null)
    {
    }
    
    public FrameHandler(Action xiEventComplete) 
      : base (xiEventComplete)
    {
    }

    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFrames = xiFrames
        .Where(frame => frame.Lights != null ||
                        frame.Fans != null ||
                        frame.Rumbles != null);

      return lFrames.Any(frame => frame != null);
    }

    public override FrameSnapshot GetNextSnapshot()
    {
      var lFrame = GetNextFrame();
      return new FrameSnapshot(lFrame, 0);
    }
  }
}
