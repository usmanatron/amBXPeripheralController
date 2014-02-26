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

    public override FrameSnapshot GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();
      return new FrameSnapshot(lFrame, 0);
    }
  }
}
