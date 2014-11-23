using aPC.Common.Entities;

namespace aPC.Common.Server.Snapshots
{
  public class FrameSnapshot : SnapshotBase
  {
    public FrameSnapshot(Frame frame, int fadeTime)
      : base(fadeTime, frame.Length)
    {
      Frame = frame;
    }

    public Frame Frame;
  }
}