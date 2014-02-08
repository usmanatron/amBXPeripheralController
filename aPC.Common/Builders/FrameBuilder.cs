using System.Collections.Generic;
using aPC.Common.Entities;
using System;
using System.Linq;

namespace aPC.Common.Builders
{
  public class FrameBuilder
  {
    public FrameBuilder()
    {
      mFrames = new List<Frame>();
    }

    public FrameBuilder AddFrame()
    {
      if (mCurrentFrame != null)
      {
        AddCurrentFrame();
      }
      SetupCurrentFrame();
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
      mIsRepeatedSpecified = true;
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
      AddCurrentFrame();
      return mFrames;
    }

    private void AddCurrentFrame()
    {
      if (CurrentFrameIsInvalid)
      {
        throw new ArgumentException("The last frame passed into FrameBuilder is invalid - please check and try again.");
      }
      mFrames.Add(mCurrentFrame);
    }

    private bool CurrentFrameIsInvalid
    {
      get
      {
        return !mIsRepeatedSpecified ||
               mCurrentFrame.Length == default(int) ||
               AllSectionsOnCurrentFrameUnSpecified;
      }
    }

    private bool AllSectionsOnCurrentFrameUnSpecified
    {
      get
      {
        return mCurrentFrame.Lights == null &&
               mCurrentFrame.Fans == null &&
               mCurrentFrame.Rumbles == null;
      }
    }

    private void SetupCurrentFrame()
    {
      mCurrentFrame  = new Frame();
      mIsRepeatedSpecified = false;
    }

    private readonly List<Frame> mFrames;
    private Frame mCurrentFrame;
    private bool mIsRepeatedSpecified;
  }
}
