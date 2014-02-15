using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace aPC.Common.Entities
{
  public static class SectionBaseExtensions
  {
    public static T GetComponentValueInDirection<T>(this SectionBase<T> xiSection, eDirection xiDirection) 
      where T : Component
    {
      var lField = xiSection.GetComponentInfoInDirection(xiDirection);
      if (lField == null)
      {
        return null;
      }

      return (T)lField.GetValue(xiSection);
    }

    private static FieldInfo GetComponentInfoInDirection<T>(this SectionBase<T> xiSection, eDirection xiDirection) 
      where T : Component
    {
      if (xiSection == null)
      {
        return null;
      }

      return GetSectionFields(xiSection)
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, xiDirection));
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

    private static IEnumerable<FieldInfo> GetSectionFields<T>(this SectionBase<T> xiSection) where T : Component
    {
      return xiSection
        .GetType()
        .GetFields();
    }
  }
}
