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
      where T : IComponent
    {
      var lField = xiSection.GetComponentInfoInDirection(xiDirection);
      return xiSection.GetComponent(lField);
    }

    public static bool SetComponentValueInDirection<T>(this SectionBase<T> xiSection, T xiComponent, eDirection xiDirection) 
      where T : IComponent
    {
      var lField = xiSection.GetComponentInfoInDirection(xiDirection);
      return xiSection.TrySetComponent(lField, xiComponent);
    }

    public static T GetPhysicalComponentValueInDirection<T>(this SectionBase<T> xiSection, eDirection xiDirection)
      where T : IComponent
    {
      var lField = xiSection.GetPhysicalComponentInfoInDirection(xiDirection);
      return xiSection.GetComponent(lField);
    }

    public static bool SetPhysicalComponentValueInDirection<T>(this SectionBase<T> xiSection, T xiComponent, eDirection xiDirection)
      where T : IComponent
    {
      var lField = xiSection.GetPhysicalComponentInfoInDirection(xiDirection);
      return xiSection.TrySetComponent(lField, xiComponent);
    }

    #region Private Helper methods

    private static FieldInfo GetComponentInfoInDirection<T>(this SectionBase<T> xiSection, eDirection xiDirection)
      where T : IComponent
    {
      if (xiSection == null)
      {
        return null;
      }

      return GetSectionFields(xiSection)
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, xiDirection));
    }

    private static FieldInfo GetPhysicalComponentInfoInDirection<T>(this SectionBase<T> xiSection, eDirection xiDirection) 
      where T : IComponent
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

    private static T GetComponent<T>(this SectionBase<T> xiSection, FieldInfo xiFieldInfo)
      where T : IComponent
    {
      if (xiFieldInfo == null)
      {
        return default(T);
      }

      return (T)xiFieldInfo.GetValue(xiSection);
    }

    private static bool TrySetComponent<T>(this SectionBase<T> xiSection, FieldInfo xiFieldInfo, T xiComponent)
      where T : IComponent
    {
      if (xiFieldInfo == null)
      {
        return false;
      }

      try
      {
        xiFieldInfo.SetValue(xiSection, xiComponent);
      }
      catch
      {
        return false;
      }
      return true;
    }

    private static IEnumerable<FieldInfo> GetSectionFields<T>(this SectionBase<T> xiSection) where T : IComponent
    {
      return xiSection
        .GetType()
        .GetFields();
    }

    #endregion
  }
}
