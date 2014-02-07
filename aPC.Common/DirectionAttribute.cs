using System;
using System.Linq;
using System.Reflection;

namespace aPC.Common
{
  [AttributeUsage(AttributeTargets.All, AllowMultiple = true)]
  class DirectionAttribute : Attribute
  {
    public DirectionAttribute(eDirection xiDirection)
    {
      mDirection = xiDirection;
    }

    public eDirection Direction
    {
      get { return mDirection; }
    }

    public static bool MatchesDirection(FieldInfo xiFieldInfo, eDirection xiDirection)
    {
      var lAttributes = xiFieldInfo.GetCustomAttributes<DirectionAttribute>();
      return lAttributes != null && lAttributes.Any(attr => attr.Direction == xiDirection);
    }

    private readonly eDirection mDirection;
  }
}
