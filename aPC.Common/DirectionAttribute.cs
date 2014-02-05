using System;

namespace aPC.Common
{
  [AttributeUsage(AttributeTargets.All)]
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

    private eDirection mDirection;
  }
}
