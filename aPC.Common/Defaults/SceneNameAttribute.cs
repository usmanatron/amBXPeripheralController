using System;
using System.Reflection;

namespace aPC.Common.Defaults
{
  [AttributeUsage(AttributeTargets.Property)]
  public class SceneNameAttribute : Attribute
  {
    public string Name { get; private set; }

    public SceneNameAttribute(string name)
    {
      Name = name;
    }

    public static string GetName(PropertyInfo propertyInfo)
    {
      return propertyInfo.GetCustomAttribute<SceneNameAttribute>().Name;
    }

    public static bool MatchesName(PropertyInfo propertyInfo, string name)
    {
      var attribute = propertyInfo.GetCustomAttribute<SceneNameAttribute>();

      if (attribute == null)
      {
        return false;
      }

      return string.Equals(attribute.Name, name, StringComparison.InvariantCultureIgnoreCase);
    }
  }
}