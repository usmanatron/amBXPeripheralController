using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class FrameHandler : SceneHandlerBase<FrameSnapshot>
  {
    public FrameHandler(amBXScene scene, Action eventComplete)
      : base(scene, eventComplete)
    {
    }

    public override FrameSnapshot GetNextSnapshot(eDirection direction)
    {
      var frame = GetNextFrame();
      return new FrameSnapshot(frame, 0);
    }
  }
}