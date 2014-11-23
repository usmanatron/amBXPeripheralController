using System;
using System.Linq;
using System.Reflection;

namespace aPC.Common
{
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
  public class DirectionAttribute : Attribute
  {
    public DirectionAttribute(eDirection direction)
    {
      Direction = direction;
    }

    public eDirection Direction
    {
      get;
      private set;
    }

    public static bool MatchesDirection(FieldInfo fieldInfo, eDirection direction)
    {
      var attributes = fieldInfo.GetCustomAttributes<DirectionAttribute>();

      return attributes != null &&
             attributes.Any(attr => attr.Direction == direction);
    }
  }
}