using aPC.Common.Entities;

namespace aPC.Server.Snapshots
{
  public class FrameSnapshot : SnapshotBase
  {
    public FrameSnapshot(Frame xiFrame, int xiFadeTime)
      : base(xiFadeTime, xiFrame.Length)
    {
      Frame = xiFrame;
    }

    public Frame Frame;
  }
}
