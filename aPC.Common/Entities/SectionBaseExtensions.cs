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

    public static FieldInfo GetPhysicalComponentInfoInDirection(this SectionBase xiSection, eDirection xiDirection)
    {
      var lFieldInrightDirection = GetComponentInfoInDirection(xiSection, xiDirection);

      return PhysicalComponentAttribute.IsPhysicalDirection(lFieldInrightDirection)
        ? lFieldInrightDirection
        :
        null;
    }

    private static IEnumerable<FieldInfo> GetSectionFields(this SectionBase xiSection)
    {
      return xiSection
        .GetType()
        .GetFields();
    }
  }
}
