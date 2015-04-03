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
      Reset();
    }

    private void Reset()
    {
      lightSection = new LightSection();
      lightSpecified = false;
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
      if (lightSection.GetComponentValueInDirection(direction) != null)
      {
        throw new ArgumentException("Attempted to add multiple lights in the same direction");
      }

      lightSection.Lights.Add(light);
      lightSpecified = true;
      return this;
    }

    private bool LightIsValid(Light light)
    {
      return light.FadeTime > 0;
    }

    public LightSection Build()
    {
      if (!LightSectionIsValid)
      {
        throw new ArgumentException("Incomplete LightSection built.  At least one light and the Fade Time must be specified.");
      }

      var builtLightSection = lightSection;
      Reset();
      return builtLightSection;
    }

    private bool LightSectionIsValid
    {
      get
      {
        return lightSpecified;
      }
    }
  }
}