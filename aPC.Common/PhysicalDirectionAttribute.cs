using System;
using System.Linq;

namespace aPC.Common
{
  /// <summary>
  /// An attribute used to decorate eDirection enums.  Adding this to
  /// a direction signifies that, under normal circumstances, this direction
  /// has a physical component associated with it.
  /// This is useful for cases where performance concerns force the lights 
  /// used to be limited.
  /// </summary>
  [AttributeUsage(AttributeTargets.All)]  //qqUMI fix this to be specific - not "all"
  public class PhysicalDirectionAttribute : Attribute
  {
    public static bool IsPhysicalDirection(eDirection xiDirection)
    {
      var lAttributes = xiDirection
        .GetType()
        .GetField(xiDirection.ToString())
        .GetCustomAttributes(typeof(PhysicalDirectionAttribute), false);

      return lAttributes != null && lAttributes.Any();
    }


  }
}
