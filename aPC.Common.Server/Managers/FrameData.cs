using aPC.Common.Entities;

namespace aPC.Common.Server.Managers
{
  public class FrameData : Data
  {
    public FrameData(Frame xiFrame, int xiFadeTime, int xiLength)
      : base(xiFadeTime, xiLength)
    {
      Frame = xiFrame;
    }

    public Frame Frame;
  }
}
