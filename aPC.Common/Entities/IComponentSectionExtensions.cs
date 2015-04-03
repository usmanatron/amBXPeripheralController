using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Entities
{
  public static class IComponentSectionExtensions
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
  }
}