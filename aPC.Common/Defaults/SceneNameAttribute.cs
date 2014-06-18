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

    public static string GetName(PropertyInfo xiPropertyInfo)
    {
      return xiPropertyInfo.GetCustomAttribute<SceneNameAttribute>().Name;
    }

    public static bool MatchesName(PropertyInfo xiPropertyInfo, string xiName)
    {
      var lAttribute = xiPropertyInfo.GetCustomAttribute<SceneNameAttribute>();

      if (lAttribute == null) return false;

      return string.Equals(lAttribute.Name, xiName, StringComparison.InvariantCultureIgnoreCase);
    }
  }
}