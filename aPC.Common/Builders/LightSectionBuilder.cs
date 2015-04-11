using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Builders
{
  public class LightSectionBuilder
  {
    private LightSection lightSection;

    public LightSectionBuilder()
    {
      Reset();
    }

    private void Reset()
    {
      lightSection = new LightSection() { Lights = new List<Light>() };
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

      if (!LightIsValid(light))
      {
        throw new ArgumentException("Input Light is invalid");
      }

      light.Direction = direction;
      lightSection.Lights.Add(light);
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
        throw new ArgumentException("Incomplete LightSection built - at least one light must be specified.");
      }

      var builtLightSection = lightSection;
      Reset();
      return builtLightSection;
    }

    private bool LightSectionIsValid
    {
      get
      {
        return lightSection.Lights.Any();
      }
    }
  }
}