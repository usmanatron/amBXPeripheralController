using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Entities
{
  public static class IComponentSectionExtensions
  {
    public static IDirectionalComponent GetComponentValueInDirection(this IComponentSection section, eDirection direction)
    {
      return section
        .GetComponents()
        .Where(component => component.Direction == direction)
        .Single(); //qqUMI Should this be a Single?
    }
  }
}