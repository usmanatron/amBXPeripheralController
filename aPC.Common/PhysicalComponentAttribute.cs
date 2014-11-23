using System;
using System.Reflection;

namespace aPC.Common
{
  /// <summary>
  /// Adding this to a component signifies that, under normal circumstances,
  /// this component has a physical precense (e.g. a light, fan)..
  /// This is useful for cases where performance concerns force the lights
  /// used to be limited.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field)]
  public class PhysicalComponentAttribute : Attribute
  {
    public static bool IsPhysicalDirection(FieldInfo fieldInfo)
    {
      var attribute = fieldInfo.GetCustomAttribute<PhysicalComponentAttribute>();
      return attribute != null;
    }
  }
}