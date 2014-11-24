using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Entities
{
  public static class IComponentSectionExtensions
  {
    public static IComponent GetComponentValueInDirection(this IComponentSection section, eDirection direction)
    {
      var field = section.GetComponentInfoInDirection(direction);
      return section.GetComponent(field);
    }

    public static bool SetComponentValueInDirection(this IComponentSection section, IComponent component, eDirection direction)
    {
      var field = section.GetComponentInfoInDirection(direction);
      return section.TrySetComponent(field, component);
    }

    public static IComponent GetPhysicalComponentValueInDirection(this IComponentSection section, eDirection direction)
    {
      var field = section.GetPhysicalComponentInfoInDirection(direction);
      return section.GetComponent(field);
    }

    public static bool SetPhysicalComponentValueInDirection(this IComponentSection section, IComponent component, eDirection direction)
    {
      var field = section.GetPhysicalComponentInfoInDirection(direction);
      return section.TrySetComponent(field, component);
    }

    #region Private Helper methods

    private static FieldInfo GetComponentInfoInDirection(this IComponentSection section, eDirection direction)
    {
      if (section == null)
      {
        return null;
      }

      return GetSectionFields(section)
        .SingleOrDefault(field => DirectionAttribute.MatchesDirection(field, direction));
    }

    private static FieldInfo GetPhysicalComponentInfoInDirection(this IComponentSection section, eDirection direction)
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

    private static IComponent GetComponent(this IComponentSection section, FieldInfo fieldInfo)
    {
      if (fieldInfo == null)
      {
        return default(IComponent);
      }

      return (IComponent)fieldInfo.GetValue(section);
    }

    private static bool TrySetComponent(this IComponentSection section, FieldInfo fieldInfo, IComponent component)
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

    private static IEnumerable<FieldInfo> GetSectionFields(this IComponentSection section)
    {
      return section
        .GetType()
        .GetFields();
    }

    #endregion Private Helper methods
  }
}