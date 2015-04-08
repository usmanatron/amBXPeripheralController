using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using aPC.SceneMigrator.EntitiesV1.Extensions;
using System;
using DirectionAttribute = aPC.SceneMigrator.EntitiesV1.DirectionAttribute;
using LightSectionV1 = aPC.SceneMigrator.EntitiesV1.LightSection;
using LightV1 = aPC.SceneMigrator.EntitiesV1.Light;

namespace aPC.SceneMigrator
{
  internal class LightSectionMigrator
  {
    public LightSection Migrate(LightSectionV1 oldLightSection)
    {
      if (oldLightSection == null)
      {
        return null;
      }

      var newLightSectionBuilder = new LightSectionBuilder();

      foreach (var direction in EnumExtensions.GetCompassDirections())
      {
        var oldLight = (LightV1)oldLightSection.GetComponentValueInDirection(direction);
        if (oldLight == null)
        {
          continue;
        }

        var newLight = new Light()
        {
          Red = oldLight.Red,
          Green = oldLight.Green,
          Blue = oldLight.Blue,
          FadeTime = oldLight.FadeTime
        };
        newLightSectionBuilder.WithLightInDirection(direction, newLight);
      }

      return newLightSectionBuilder.Build();
    }
  }
}