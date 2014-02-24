using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Entities;
using aPC.Common.Builders;

namespace aPC.Client.Disco.Generators
{
  public class RandomSceneGenerator : IGenerator<amBXScene>
  {
    public RandomSceneGenerator(Settings xiSettings, IGenerator<LightSection> xiLightSectionGenerator)
    {
      mSettings = xiSettings;
      mRandomLightSectionGenerator = xiLightSectionGenerator;
    }

    public amBXScene Generate()
    {
      var lScene = new amBXScene
      {
        IsEvent = false,
        IsSynchronised = false
      };

      var lFrames = new FrameBuilder();

      for (int i = 0; i < mSettings.FramesPerScene; i++ )
      {
        AddNewFrame(lFrames);
      }

      lScene.Frames = lFrames.Build();
      return lScene;
    }

    private void AddNewFrame(FrameBuilder xiBuilder)
    {
      xiBuilder
        .AddFrame()
        .WithFrameLength(mSettings.PushInterval)
        .WithRepeated(true)
        .WithLightSection(mRandomLightSectionGenerator.Generate())
        // Both fans and rumble are currently unsupported
        .WithFanSection(aPC.Common.Defaults.DefaultFanSections.Off)
        .WithRumbleSection(aPC.Common.Defaults.DefaultRumbleSections.Off);
    }

    private Settings mSettings;
    private IGenerator<LightSection> mRandomLightSectionGenerator;
  }
}
