using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace aPC.Common.Entities
{
  public static class SectionBaseExtensions
  {
    public static FieldInfo GetComponentInfoInDirection(this SectionBase xiSection, eDirection xiDirection)
    {
      return GetSectionFields(xiSection)
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, xiDirection));
    }

    private static IEnumerable<FieldInfo> GetSectionFields(this SectionBase xiSection)
    {
      return xiSection
        .GetType()
        .GetFields();
    }

    public static FieldInfo GetPhysicalComponentInfoInDirection(this SectionBase xiSection, eDirection xiDirection)
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
