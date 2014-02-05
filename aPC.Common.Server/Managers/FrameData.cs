using aPC.Common.Entities;

namespace aPC.Common.Server.Managers
{
  public class FrameData : Data
  {
    public FrameData(Frame xiFrame, int xiFadeTime)
      : base(xiFadeTime, xiFrame.Length)
    {
      Frame = xiFrame;
    }

    public Frame Frame;
  }
}
