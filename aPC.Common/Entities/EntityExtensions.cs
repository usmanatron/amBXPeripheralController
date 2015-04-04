using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Entities
{
  public static class EntityExtensions
  {
    public static DirectionalComponent GetComponentValueInDirection(this IComponentSection section, eDirection direction)
    {
      if (section == null)
      {
        return null;
      }

      return section
        .GetComponents()
        .Where(component => component.Direction == direction)
        .SingleOrDefault();
    }

    public static DirectionalComponent GetComponentInDirection(this Frame frame, eComponentType componentType, eDirection direction)
    {
      switch (componentType)
      {
        case eComponentType.Light:
          return frame.LightSection.GetComponentValueInDirection(direction);
        case eComponentType.Fan:
          return frame.FanSection.GetComponentValueInDirection(direction);
        case eComponentType.Rumble:
          return frame.RumbleSection.GetComponentValueInDirection(direction);
        default:
          throw new ArgumentException("Unexpected Component Type");
      }
    }
  }
}