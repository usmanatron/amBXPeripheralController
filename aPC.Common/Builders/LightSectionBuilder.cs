using aPC.Common.Entities;
using System;

namespace aPC.Common.Builders
{
  public class LightSectionBuilder
  {
    private LightSection lightSection;
    private bool lightSpecified;

    public LightSectionBuilder()
    {
      lightSection = new LightSection();
      lightSpecified = false;
    }

    public LightSectionBuilder WithFadeTime(int fadeTime)
    {
      lightSection.FadeTime = fadeTime;
      return this;
    }

    public LightSectionBuilder WithAllLights(Light light)
    {
      WithLightInDirection(eDirection.North, light);
      WithLightInDirection(eDirection.NorthEast, light);
      WithLightInDirection(eDirection.East, light);
      WithLightInDirection(eDirection.SouthEast, light);
      WithLightInDirection(eDirection.South, light);
      WithLightInDirection(eDirection.SouthWest, light);
      WithLightInDirection(eDirection.West, light);
      WithLightInDirection(eDirection.NorthWest, light);

      return this;
    }

    public LightSectionBuilder WithLightInDirection(eDirection direction, Light light)
    {
      var lightExists = lightSection.SetComponentValueInDirection(light, direction);
      if (!lightExists)
      {
        throw new InvalidOperationException("Attempted to update a light which does not exist");
      }
      lightSpecified = true;
      return this;
    }

    public LightSectionBuilder WithLightInDirectionIfPhysical(eDirection direction, Light light)
    {
      var lightExists = lightSection.SetPhysicalComponentValueInDirection(light, direction);
      if (lightExists)
      {
        lightSpecified = true;
      }
      return this;
    }

    public LightSection Build()
    {
      if (!LightSectionIsValid)
      {
        throw new ArgumentException("Incomplete LightSection built.  At least one light and the Fade Time must be specified.");
      }

      return lightSection;
    }

    private bool LightSectionIsValid
    {
      get
      {
        return lightSection.FadeTime != default(int) && lightSpecified;
      }
    }
  }
}