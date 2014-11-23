using aPC.Client.Morse.Codes;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using System.Collections.Generic;

namespace aPC.Client.Morse
{
  /// <summary>
  /// An extension of FrameBuilder, with some extra methods to handle
  /// reading IMorseBlocks.
  /// </summary>
  public class MorseFrameBuilder : FrameBuilder
  {
    public MorseFrameBuilder(Settings xiSettings)
      : base()
    {
      mSettings = xiSettings;
    }

    public MorseFrameBuilder AddFrames(IEnumerable<IMorseBlock> xiBlocks)
    {
      foreach (var lBlock in xiBlocks)
      {
        AddFrame(lBlock);
      }

      return this;
    }

    private MorseFrameBuilder AddFrame(IMorseBlock xiBlock)
    {
      this.AddFrame()
        .WithRepeated(mSettings.RepeatMessage)
        .WithFrameLength(xiBlock.Length * mSettings.UnitLength);

      if (mSettings.LightsEnabled)
      {
        this.WithLightSection(GetLightSection(xiBlock.Enabled));
      }
      if (mSettings.RumblesEnabled)
      {
        this.WithRumbleSection(GetRumbleSection(xiBlock.Enabled));
      }

      return this;
    }

    private LightSection GetLightSection(bool xiEnabled)
    {
      var lLight = xiEnabled ? mSettings.Colour : DefaultLights.Off;

      return new LightSectionBuilder()
        .WithFadeTime(10)
        .WithAllLights(lLight)
        .Build();
    }

    private RumbleSection GetRumbleSection(bool xiEnabled)
    {
      var lRumble = xiEnabled ? mSettings.Rumble : DefaultRumbles.Off;

      return new RumbleSectionBuilder()
        .WithFadeTime(10)
        .WithRumble(lRumble)
        .Build();
    }

    private Settings mSettings;
  }
}