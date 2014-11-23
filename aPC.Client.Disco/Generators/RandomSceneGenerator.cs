using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Client.Disco.Generators
{
  public class RandomSceneGenerator : IGenerator<amBXScene>
  {
    public RandomSceneGenerator(Settings settings, IGenerator<LightSection> lightSectionGenerator)
    {
      this.settings = settings;
      randomLightSectionGenerator = lightSectionGenerator;
    }

    public amBXScene Generate()
    {
      var scene = new amBXScene
      {
        SceneType = eSceneType.Desync
      };

      var frames = new FrameBuilder();

      for (int i = 0; i < settings.FramesPerScene; i++)
      {
        AddNewFrame(frames);
      }

      scene.Frames = frames.Build();
      return scene;
    }

    private void AddNewFrame(FrameBuilder xiBuilder)
    {
      xiBuilder
        .AddFrame()
        .WithFrameLength(settings.PushInterval)
        .WithRepeated(true)
        .WithLightSection(randomLightSectionGenerator.Generate());
    }

    private readonly Settings settings;
    private readonly IGenerator<LightSection> randomLightSectionGenerator;
  }
}