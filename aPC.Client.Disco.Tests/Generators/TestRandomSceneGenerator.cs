using aPC.Client.Disco.Generators;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Client.Disco.Tests.Generators
{
  internal class TestRandomSceneGenerator : IGenerator<amBXScene>
  {
    public TestRandomSceneGenerator(TestLightSectionGenerator xiLights)
    {
      mLights = xiLights;
    }

    public amBXScene Generate()
    {
      var lFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(1000)
        .WithLightSection(mLights.Generate())
        .Build();

      return new amBXScene
      {
        Frames = lFrames,
        IsExclusive = false,
        SceneType = eSceneType.Desync
      };
    }

    private readonly TestLightSectionGenerator mLights;
  }
}