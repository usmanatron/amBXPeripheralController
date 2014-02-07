using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Entities;
using aPC.Common.Builders;

namespace aPC.Client.Disco.Generators
{
  class RandomSceneGenerator
  {
    public RandomSceneGenerator(Settings xiSettings, Random xiRandom)
    {
      mSettings = xiSettings;
      mRandom = xiRandom;
      mRandomLightSectionGenerator = new RandomLightSectionGenerator(mSettings, mRandom);
    }

    public amBXScene Get()
    {
      var lScene = new amBXScene
      {
        IsEvent = false,
        IsSynchronised = false
      };

      var lFrames = new FrameBuilder();

      for (int i = 0; i < 2; i++ )
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
        .WithLightSection(mRandomLightSectionGenerator.Generate());

    }

    private Settings mSettings;
    private Random mRandom;
    private RandomLightSectionGenerator mRandomLightSectionGenerator;
  }
}
