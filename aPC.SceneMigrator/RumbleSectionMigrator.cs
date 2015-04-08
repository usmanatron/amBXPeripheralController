using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using System;
using DirectionAttribute = aPC.SceneMigrator.EntitiesV1.DirectionAttribute;
using RumbleSectionV1 = aPC.SceneMigrator.EntitiesV1.RumbleSection;
using RumbleV1 = aPC.SceneMigrator.EntitiesV1.Rumble;

namespace aPC.SceneMigrator
{
  internal class RumbleSectionMigrator
  {
    public RumbleSection Migrate(RumbleSectionV1 oldRumbleSection)
    {
      if (oldRumbleSection == null)
      {
        return null;
      }

      var newRumbleSectionBuilder = new RumbleSectionBuilder();

      var oldRumble = oldRumbleSection.Rumble;
      if (oldRumble == null)
      {
        return null;
      }

      var newRumble = new Rumble()
      {
        RumbleType = oldRumble.RumbleType,
        Intensity = oldRumble.Intensity,
        Speed = oldRumble.Speed
      };

      newRumbleSectionBuilder.WithRumbleInDirection(eDirection.Center, newRumble);
      return newRumbleSectionBuilder.Build();
    }
  }
}