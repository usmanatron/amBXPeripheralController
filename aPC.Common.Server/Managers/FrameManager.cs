using aPC.Common.Entities;
using System.Linq;
using System;
using System.Collections.Generic;

namespace aPC.Common.Server.Managers
{
  public class FrameManager : ManagerBase
  {
    public FrameManager() 
      : this(null)
    {
    }

    public FrameManager(Action xiEventComplete)
      : base(xiEventComplete)
    {
      SetupNewScene(CurrentScene);
    }

    protected override bool FramesAreApplicable(List<Frame> xiFrames)
    {
      var lFrames = xiFrames
        .Where(frame => frame.Lights != null || 
                        frame.Fans   != null || 
                        frame.Rumbles != null);

      return lFrames.Any(frame => frame != null);
    }

    public override Data GetNext()
    {
      var lFrame = GetNextFrame();
      return new FrameData(lFrame, 0);
    }
  }
}
