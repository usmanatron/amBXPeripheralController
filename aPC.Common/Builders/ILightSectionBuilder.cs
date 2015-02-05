using aPC.Common.Entities;
using System;

namespace aPC.Common.Builders
{
  public interface ILightSectionBuilder
  {
    ILightSectionBuilder WithAllLights(Light light);

    ILightSectionBuilder WithLightInDirection(eDirection direction, Light light);

    ILightSectionBuilder WithLightInDirectionIfPhysical(eDirection direction, Light light);

    LightSection Build();
  }
}