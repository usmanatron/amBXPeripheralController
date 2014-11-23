using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Entities
{
  public static class SectionBaseExtensions
  {
    public static T GetComponentValueInDirection<T>(this SectionBase<T> section, eDirection direction)
      where T : IComponent
    {
      var field = section.GetComponentInfoInDirection(direction);
      return section.GetComponent(field);
    }

    public static bool SetComponentValueInDirection<T>(this SectionBase<T> section, T component, eDirection direction)
      where T : IComponent
    {
      var field = section.GetComponentInfoInDirection(direction);
      return section.TrySetComponent(field, component);
    }

    public static T GetPhysicalComponentValueInDirection<T>(this SectionBase<T> section, eDirection direction)
      where T : IComponent
    {
      var field = section.GetPhysicalComponentInfoInDirection(direction);
      return section.GetComponent(field);
    }

    public static bool SetPhysicalComponentValueInDirection<T>(this SectionBase<T> section, T component, eDirection direction)
      where T : IComponent
    {
      var field = section.GetPhysicalComponentInfoInDirection(direction);
      return section.TrySetComponent(field, component);
    }

    #region Private Helper methods

    private static FieldInfo GetComponentInfoInDirection<T>(this SectionBase<T> section, eDirection direction)
      where T : IComponent
    {
      if (section == null)
      {
        return null;
      }

      return GetSectionFields(section)
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, direction));
    }

    private static FieldInfo GetPhysicalComponentInfoInDirection<T>(this SectionBase<T> section, eDirection direction)
      where T : IComponent
    {
      var fieldInDirection = GetComponentInfoInDirection(section, direction);
      if (fieldInDirection == null)
      {
        return null;
      }

      return PhysicalComponentAttribute.IsPhysicalDirection(fieldInDirection)
        ? fieldInDirection
        : null;
    }

    private static T GetComponent<T>(this SectionBase<T> section, FieldInfo fieldInfo)
      where T : IComponent
    {
      if (fieldInfo == null)
      {
        return default(T);
      }

      return (T)fieldInfo.GetValue(section);
    }

    private static bool TrySetComponent<T>(this SectionBase<T> section, FieldInfo fieldInfo, T component)
      where T : IComponent
    {
      if (fieldInfo == null)
      {
        return false;
      }

      try
      {
        fieldInfo.SetValue(section, component);
      }
      catch
      {
        return false;
      }
      return true;
    }

    private static IEnumerable<FieldInfo> GetSectionFields<T>(this SectionBase<T> section) where T : IComponent
    {
      return section
        .GetType()
        .GetFields();
    }

    #endregion Private Helper methods
  }
}