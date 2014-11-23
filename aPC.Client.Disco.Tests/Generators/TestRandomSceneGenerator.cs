using aPC.Client.Disco.Generators;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Client.Disco.Tests.Generators
{
  internal class TestRandomSceneGenerator : IGenerator<amBXScene>
  {
    private readonly TestLightSectionGenerator lights;

    public TestRandomSceneGenerator(TestLightSectionGenerator lights)
    {
      this.lights = lights;
    }

    public amBXScene Generate()
    {
      var frames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(1000)
        .WithLightSection(lights.Generate())
        .Build();

      return new amBXScene
      {
        Frames = frames,
        IsExclusive = false,
        SceneType = eSceneType.Desync
      };
    }
  }
}