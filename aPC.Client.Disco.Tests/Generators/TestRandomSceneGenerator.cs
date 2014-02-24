using aPC.Client.Disco.Generators;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Client.Disco.Tests.Generators
{
  class TestRandomSceneGenerator : IGenerator<amBXScene>
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
        IsEvent = false,
        IsExclusive = false,
        IsSynchronised = false
      };
    }

    private readonly TestLightSectionGenerator mLights;
  }
}
