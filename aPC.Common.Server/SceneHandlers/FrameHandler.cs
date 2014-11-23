using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class FrameHandler : SceneHandlerBase<FrameSnapshot>
  {
    public FrameHandler(amBXScene xiScene, Action xiEventComplete)
      : base(xiScene, xiEventComplete)
    {
    }

    public override FrameSnapshot GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();
      return new FrameSnapshot(lFrame, 0);
    }
  }
}