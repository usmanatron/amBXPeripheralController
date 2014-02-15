using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace aPC.Common.Entities
{
  public static class SectionBaseExtensions
  {
    public static FieldInfo GetComponentInfoInDirection<T>(this SectionBase<T> xiSection, eDirection xiDirection) where T : Component
    {
      if (xiSection == null)
      {
        return null;
      }

      return GetSectionFields(xiSection)
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, xiDirection));
    }

    private static IEnumerable<FieldInfo> GetSectionFields<T>(this SectionBase<T> xiSection) where T : Component
    {
      return xiSection
        .GetType()
        .GetFields();
    }

    public static FieldInfo GetPhysicalComponentInfoInDirection<T>(this SectionBase<T> xiSection, eDirection xiDirection) where T : Component
    {
      var lFieldInDirection = GetComponentInfoInDirection(xiSection, xiDirection);

      if (lFieldInDirection == null)
      {
        return null;
      }

      return PhysicalComponentAttribute.IsPhysicalDirection(lFieldInDirection)
        ? lFieldInDirection
        : null;
    }
  }
}
