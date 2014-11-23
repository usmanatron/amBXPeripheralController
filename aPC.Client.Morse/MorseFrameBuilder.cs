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
    private Settings settings;

    public MorseFrameBuilder(Settings settings)
      : base()
    {
      this.settings = settings;
    }

    public MorseFrameBuilder AddFrames(IEnumerable<IMorseBlock> blocks)
    {
      foreach (var block in blocks)
      {
        AddFrame(block);
      }

      return this;
    }

    private MorseFrameBuilder AddFrame(IMorseBlock block)
    {
      this.AddFrame()
        .WithRepeated(settings.RepeatMessage)
        .WithFrameLength(block.Length * settings.UnitLength);

      if (settings.LightsEnabled)
      {
        this.WithLightSection(GetLightSection(block.Enabled));
      }
      if (settings.RumblesEnabled)
      {
        this.WithRumbleSection(GetRumbleSection(block.Enabled));
      }

      return this;
    }

    private LightSection GetLightSection(bool fansEnabled)
    {
      var lLight = fansEnabled ? settings.Colour : DefaultLights.Off;

      return new LightSectionBuilder()
        .WithFadeTime(10)
        .WithAllLights(lLight)
        .Build();
    }

    private RumbleSection GetRumbleSection(bool rumbleEnabled)
    {
      var lRumble = rumbleEnabled ? settings.Rumble : DefaultRumbles.Off;

      return new RumbleSectionBuilder()
        .WithFadeTime(10)
        .WithRumble(lRumble)
        .Build();
    }
  }
}