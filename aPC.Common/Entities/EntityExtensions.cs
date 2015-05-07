using System;
using System.Linq;

namespace aPC.Common.Entities
{
  public static class EntityExtensions
  {
    public static DirectionalComponent GetComponentSectionInDirection(this IComponentSection section, eDirection direction)
    {
      if (section == null)
      {
        return null;
      }

      return section
        .GetComponents()
        .SingleOrDefault(component => component.Direction == direction);
    }

    public static DirectionalComponent GetComponentInDirection(this Frame frame, eComponentType componentType, eDirection direction)
    {
      switch (componentType)
      {
        case eComponentType.Light:
          return frame.LightSection.GetComponentSectionInDirection(direction);

        case eComponentType.Fan:
          return frame.FanSection.GetComponentSectionInDirection(direction);

        case eComponentType.Rumble:
          return frame.RumbleSection.GetComponentSectionInDirection(direction);

        default:
          throw new ArgumentException("Unexpected Component Type");
      }
    }
  }
}