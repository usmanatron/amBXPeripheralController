using aPC.Common.Entities;
using System;
using System.Collections.Generic;

namespace aPC.Common.Builders
{
  /// <summary>
  ///   Assists with building a list of frames.
  /// </summary>
  /// <remarks>
  ///   An important Gotcha: when creating a new Builder, you must first call AddFrame
  ///   before calling anything else to complete iitialisation.
  ///   TODO: Fix this limitation
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
      SetupCurrentFrame();
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
               AtLeastOneSectionSpecified;
      }
    }

    private bool AtLeastOneSectionSpecified
    {
      get
      {
        return currentFrame.Lights != null ||
               currentFrame.Fans != null ||
               currentFrame.Rumbles != null;
      }
    }

    private void SetupCurrentFrame()
    {
      currentFrame = new Frame();
      isRepeatedSpecified = false;
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
      currentFrame.Lights = lightSection;
      return this;
    }

    public FrameBuilder WithFanSection(FanSection fanSection)
    {
      currentFrame.Fans = fanSection;
      return this;
    }

    public FrameBuilder WithRumbleSection(RumbleSection rumbleSection)
    {
      currentFrame.Rumbles = rumbleSection;
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