using aPC.Common.Entities;
using System;
using System.Collections.Generic;

namespace aPC.Common.Builders
{
  /// <summary>
  ///   Assists with building a list of frames.
  /// </summary>
  /// <remarks>
  ///   An important gotcha: when creating a new Builder, you must first call AddFrame
  ///   before building your first Frame.
  /// </remarks>
  public class FrameBuilder
  {
    private List<Frame> frames;
    private Frame currentFrame;
    private bool isRepeatedSpecified;

    public FrameBuilder()
    {
      Reset();
    }

    private void Reset()
    {
      frames = new List<Frame>();
    }

    public FrameBuilder AddFrame()
    {
      if (currentFrame != null)
      {
        AddCurrentFrame();
      }

      // Setup current Frame
      currentFrame = new Frame();
      isRepeatedSpecified = false;

      return this;
    }

    private void AddCurrentFrame()
    {
      if (!CurrentFrameIsValid)
      {
        throw new ArgumentException("The last frame passed into FrameBuilder is invalid - please check and try again.");
      }
      frames.Add(currentFrame);
    }

    private bool CurrentFrameIsValid
    {
      get
      {
        return isRepeatedSpecified &&
               currentFrame.Length != default(int) &&
               AtLeastOneComponentSpecified;
      }
    }

    private bool AtLeastOneComponentSpecified
    {
      get
      {
        return currentFrame.LightSection != null ||
               currentFrame.FanSection != null ||
               currentFrame.RumbleSection != null;
      }
    }

    public FrameBuilder WithFrameLength(int length)
    {
      currentFrame.Length = length;
      return this;
    }

    public FrameBuilder WithRepeated(bool isRepeated)
    {
      currentFrame.IsRepeated = isRepeated;
      isRepeatedSpecified = true;
      return this;
    }

    public FrameBuilder WithLightSection(LightSection lightSection)
    {
      currentFrame.LightSection = lightSection;
      return this;
    }

    public FrameBuilder WithFanSection(FanSection fanSection)
    {
      currentFrame.FanSection = fanSection;
      return this;
    }

    public FrameBuilder WithRumbleSection(RumbleSection rumbleSection)
    {
      currentFrame.RumbleSection = rumbleSection;
      return this;
    }

    public List<Frame> Build()
    {
      AddCurrentFrame();

      var builtFrames = frames;
      Reset();
      return builtFrames;
    }
  }
}