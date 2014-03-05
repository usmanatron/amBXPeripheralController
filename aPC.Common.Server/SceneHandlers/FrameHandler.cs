using System.Collections.Generic;
using System.Linq;
using aPC.Common.Server.Snapshots;
using aPC.Common.Entities;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class FrameHandler : SceneHandlerBase<FrameSnapshot>
  {
    public FrameHandler(amBXScene xiScene)
      : this(xiScene, null)
    {
    }
    
    public FrameHandler(amBXScene xiScene, Action xiEventComplete) 
      : base (xiScene, xiEventComplete)
    {
    }

    public override FrameSnapshot GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();
      return new FrameSnapshot(lFrame, 0);
    }
  }
}
