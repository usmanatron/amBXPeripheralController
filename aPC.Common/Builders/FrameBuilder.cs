using System.Collections.Generic;
using aPC.Common.Entities;

namespace aPC.Common.Builders
{
  class FrameBuilder
  {
    public FrameBuilder()
    {
      mFrames = new List<Frame>();
    }

    public FrameBuilder AddFrame()
    {
      if (mCurrentFrame != null)
      {
        mFrames.Add(mCurrentFrame);
      }
      mCurrentFrame  = new Frame();
      return this;
    }

    public FrameBuilder WithFrameLength(int xiLength)
    {
      mCurrentFrame.Length = xiLength;
      return this;
    }

    public FrameBuilder WithRepeated(bool xiIsRepeated)
    {
      mCurrentFrame.IsRepeated = xiIsRepeated;
      return this;
    }

    public FrameBuilder WithLightSection(LightSection xiLightSection)
    {
      mCurrentFrame.Lights = xiLightSection;
      return this;
    }

    public FrameBuilder WithFanSection(FanSection xiFanSection)
    {
      mCurrentFrame.Fans = xiFanSection;
      return this;
    }

    public FrameBuilder WithRumbleSection(RumbleSection xiRumbleSection)
    {
      mCurrentFrame.Rumbles = xiRumbleSection;
      return this;
    }

    public List<Frame> Build()
    {
      mFrames.Add(mCurrentFrame);
      return mFrames;
    }

    private readonly List<Frame> mFrames;
    private Frame mCurrentFrame;
  }
}
