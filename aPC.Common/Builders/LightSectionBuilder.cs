using aPC.Common.Entities;
using System;
using System.Collections.Generic;

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
      lightSection = new LightSection() { Lights = new List<Light>() };
      lightSpecified = false;
    }

    public LightSectionBuilder WithAllLights(Light light)
    {
      return WithLightInDirections(EnumExtensions.GetCompassDirections(), light);
    }

    public LightSectionBuilder WithLightInDirections(IEnumerable<eDirection> directions, Light light)
    {
      foreach (var direction in directions)
      {
        var directionalLight = (Light)light.Clone();
        WithLightInDirection(direction, directionalLight);
      }

      return this;
    }

    public LightSectionBuilder WithLightInDirection(eDirection direction, Light light)
    {
      if (lightSection.GetComponentSectionInDirection(direction) != null)
      {
        throw new ArgumentException("Attempted to add multiple lights in the same direction");
      }

      light.Direction = direction;
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