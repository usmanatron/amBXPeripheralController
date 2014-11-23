using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using System;
using System.Linq;

namespace aPC.Client.Disco.Generators
{
  public class RandomLightSectionGenerator : IGenerator<LightSection>
  {
    public RandomLightSectionGenerator(Settings settings, Random random)
    {
      this.settings = settings;
      this.random = random;
    }

    public LightSection Generate()
    {
      var fadeTime = (int)settings.FadeTime.GetScaledValue(random.NextDouble());
      var sectionBuilder = new LightSectionBuilder().WithFadeTime(fadeTime);

      foreach (eDirection direction in Enum.GetValues(typeof(eDirection)).Cast<eDirection>())
      {
        sectionBuilder.WithLightInDirectionIfPhysical(direction, GetRandomLight());
      }

      return sectionBuilder.Build();
    }

    private Light GetRandomLight()
    {
      return random.NextDouble() < settings.ChangeThreshold
        ? null
        : new Light
      {
        Red = settings.RedColourWidth.GetScaledValue(random.NextDouble()),
        Blue = settings.BlueColourWidth.GetScaledValue(random.NextDouble()),
        Green = settings.GreenColourWidth.GetScaledValue(random.NextDouble()),
        Intensity = settings.LightIntensityWidth.GetScaledValue(random.NextDouble())
      };
    }

    private readonly Random random;
    private readonly Settings settings;
  }
}