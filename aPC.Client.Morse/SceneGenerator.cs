using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aPC.Client.Morse
{
  public class SceneGenerator
  {
    public SceneGenerator(Settings xiSettings)
    {
      mSettings = xiSettings;
    }

    public amBXScene Generate()
    {
      var lScene = new amBXScene()
        {
          SceneType = mSettings.RepeatMessage ? eSceneType.Sync : eSceneType.Event
        };

      var lFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(mSettings.RepeatMessage)
        .WithFrameLength(mSettings.UnitLength)
        .WithLightSection(GetLightSection())
        .WithRumbleSection(GetRumbleSection())
        .Build();

      lScene.Frames = lFrames;
      return lScene;
    }

    private LightSection GetLightSection()
    {
      if (!mSettings.LightsEnabled)
      {
        return null;
      }

      return new LightSectionBuilder()
        .WithFadeTime(10)
        .WithAllLights(mSettings.Colour)
        .Build();
    }

    private RumbleSection GetRumbleSection()
    {
      if (!mSettings.RumblesEnabled)
      {
        return null;
      }

      return new RumbleSectionBuilder()
        .WithFadeTime(10)
        .WithRumble(DefaultRumbles.Thunder)
        .Build();
    }

    private Settings mSettings;
  }
}
