using System;
using System.Reflection;

namespace aPC.Common
{
  /// <summary>
  /// Adding this to
  /// a component signifies that, under normal circumstances, this compnent
  /// has a physical item (e.g. a light, fan) associated with it.
  /// This is useful for cases where performance concerns force the lights 
  /// used to be limited.
  /// </summary>
  [AttributeUsage(AttributeTargets.Field)] 
  public class PhysicalComponentAttribute : Attribute
  {
    public static bool IsPhysicalDirection(FieldInfo xiFieldInfo)
    {
      var lAttribute = xiFieldInfo.GetCustomAttribute<PhysicalComponentAttribute>();
      return lAttribute != null;
    }
  }
}
