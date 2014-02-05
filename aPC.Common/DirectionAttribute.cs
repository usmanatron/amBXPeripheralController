using System;
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
      var lAttribute = xiFieldInfo.GetCustomAttribute<DirectionAttribute>();
      return lAttribute != null && lAttribute.Direction == xiDirection;
    }

    private readonly eDirection mDirection;
  }
}
