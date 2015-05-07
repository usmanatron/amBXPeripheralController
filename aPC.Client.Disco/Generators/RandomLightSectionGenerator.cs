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
    private readonly Settings settings;
    private readonly LightSectionBuilder lightSectionBuilder;
    private readonly Random random;

    public RandomLightSectionGenerator(Settings settings, LightSectionBuilder lightSectionBuilder, Random random)
    {
      this.settings = settings;
      this.lightSectionBuilder = lightSectionBuilder;
      this.random = random;
    }

    public LightSection Generate()
    {
      var lights = BuildLights();
      var lightsSubset = RemoveSubset(lights);

      foreach (var light in lightsSubset)
      {
        lightSectionBuilder.WithLightInDirection(light.Direction, light);
      }

      return lightSectionBuilder.Build();
    }

    private IEnumerable<Light> BuildLights()
    {
      return EnumExtensions.GetCompassDirections()
        .Select(GetRandomLightInDirection);
    }

    /// <summary>
    ///   Remove some of the given lights to make updates even more random
    /// </summary>
    /// <remarks>
    ///   Use the ChangeThreshold to decide how many lights we should remove.
    ///   A larger change threshold implies more updates
    /// </remarks>
    private IEnumerable<Light> RemoveSubset(IEnumerable<Light> lights)
    {
      var numberOfLightsToKeep = (int)Math.Ceiling(settings.ChangeThreshold * lights.Count());
      return lights.OrderBy(light => light.Red).Take(numberOfLightsToKeep);
    }

    private Light GetRandomLightInDirection(eDirection direction)
    {
      return new Light
      {
        Direction = direction,
        Red = settings.RedColourWidth.GetScaledValue(random.NextDouble()),
        Blue = settings.BlueColourWidth.GetScaledValue(random.NextDouble()),
        Green = settings.GreenColourWidth.GetScaledValue(random.NextDouble()),
        FadeTime = (int)settings.FadeTime.GetScaledValue(random.NextDouble())
      };
    }
  }
}