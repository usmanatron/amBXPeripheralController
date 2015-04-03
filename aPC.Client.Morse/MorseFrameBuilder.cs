using aPC.Client.Morse.Codes;
using aPC.Common;
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
    private LightSectionBuilder lightSectionBuilder;
    private RumbleSectionBuilder rumbleSectionBuilder;

    public MorseFrameBuilder(LightSectionBuilder lightSectionBuilder, RumbleSectionBuilder rumbleSectionBuilder)
      : base()
    {
      this.lightSectionBuilder = lightSectionBuilder;
      this.rumbleSectionBuilder = rumbleSectionBuilder;
    }

    public MorseFrameBuilder AddFrames(Settings settings, IEnumerable<IMorseBlock> blocks)
    {
      foreach (var block in blocks)
      {
        AddFrame(settings, block);
      }

      return this;
    }

    private MorseFrameBuilder AddFrame(Settings settings, IMorseBlock block)
    {
      this.AddFrame()
        .WithRepeated(settings.RepeatMessage)
        .WithFrameLength(block.Length * settings.UnitLength);

      if (settings.LightsEnabled)
      {
        this.WithLightSection(GetLightSection(block.Enabled, settings.Colour));
      }
      if (settings.RumblesEnabled)
      {
        this.WithRumbleSection(GetRumbleSection(block.Enabled, settings.Rumble));
      }

      return this;
    }

    private LightSection GetLightSection(bool lightEnabled, Light enabledColour)
    {
      var light = lightEnabled
        ? enabledColour
        : DefaultLights.Off;

      return lightSectionBuilder.WithAllLights(light)
        .Build();
    }

    private RumbleSection GetRumbleSection(bool rumbleEnabled, Rumble enabledRumble)
    {
      var rumble = rumbleEnabled
        ? enabledRumble
        : DefaultRumbles.Off;

      return rumbleSectionBuilder.WithRumbleInDirection(eDirection.Center, rumble)
        .Build();
    }
  }
}