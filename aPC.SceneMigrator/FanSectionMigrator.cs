using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using aPC.SceneMigrator.EntitiesV1.Extensions;
using System;
using DirectionAttribute = aPC.SceneMigrator.EntitiesV1.DirectionAttribute;
using FanSectionV1 = aPC.SceneMigrator.EntitiesV1.FanSection;
using FanV1 = aPC.SceneMigrator.EntitiesV1.Fan;

namespace aPC.SceneMigrator
{
  internal class FanSectionMigrator
  {
    public FanSection Migrate(FanSectionV1 oldFanSection)
    {
      if (oldFanSection == null)
      {
        return null;
      }

      var newFanSectionBuilder = new FanSectionBuilder();

      foreach (var direction in EnumExtensions.GetCompassDirections())
      {
        var oldFan = (FanV1)oldFanSection.GetComponentValueInDirection(direction);
        if (oldFan == null)
        {
          continue;
        }

        var newFan = new Fan()
        {
          Intensity = oldFan.Intensity
        };
        newFanSectionBuilder.WithFanInDirection(direction, newFan);
      }

      return newFanSectionBuilder.Build();
    }
  }
}