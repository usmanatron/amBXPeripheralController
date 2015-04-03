using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Disco.Generators
{
  public class RandomLightSectionGenerator : IGenerator<LightSection>
  {
    private readonly Random random;
    private readonly Settings settings;

    public RandomLightSectionGenerator(Settings settings, Random random)
    {
      this.settings = settings;
      this.random = random;
    }

    public LightSection Generate()
    {
      var fadeTime = (int)settings.FadeTime.GetScaledValue(random.NextDouble());
      var sectionBuilder = new LightSectionBuilder();

      var physicalDirections = new List<eDirection> { eDirection.West, eDirection.NorthWest, eDirection.North, eDirection.NorthEast, eDirection.East };
      foreach (eDirection direction in physicalDirections)
      {
        sectionBuilder.WithLightInDirection(direction, GetRandomLight());
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
        FadeTime = 10 // Light updates are nearly instant
      };
    }
  }
}