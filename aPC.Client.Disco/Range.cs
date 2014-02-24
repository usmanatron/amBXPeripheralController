using System;

namespace aPC.Client.Disco
{
  /// <summary>
  /// Describes a min - max range of values.
  /// </summary>
  public class Range
  {
    public Range(float xiMinimum, float xiMaximum)
    {
      if (xiMaximum < xiMinimum)
      {
        var lMessage = string.Format(
          "Range constructor values in the wrong order: " + Environment.NewLine +
          "Minimum: {0} " + Environment.NewLine +
          "Maximum: {1}", 
          xiMinimum, 
          xiMaximum);
        throw new ArgumentException(lMessage);
      }

      mMinimum = xiMinimum;
      mMaximum = xiMaximum;
    }

    /// <remarks>
    ///   xiValue is expected to be between 0 and 1.  If this 
    ///   isn't the case, we clip the value appropriately instead of
    ///   throwing any error.
    /// </remarks>
    public float GetScaledValue(double xiValue)
    {
      var lValue = CheckValueAndClipIfNecessary(xiValue);

      return ((float)lValue * Width) + mMinimum;
    }

    private double CheckValueAndClipIfNecessary(double xiValue)
    {
      if (xiValue < 0)
      {
        return 0;
      }
      if (1 < xiValue)
      {
        return 1;
      }

      return xiValue;
    }

    public float Width
    {
      get
      {
        return mMaximum - mMinimum;
      }
    }

    public override bool Equals(object obj)
    {
      var lOther = obj as Range;

      if (lOther == null)
      {
        return false;
      }

      return lOther.mMinimum == mMinimum &&
             lOther.mMaximum == mMaximum;
    }

    public override int GetHashCode()
    {
      return mMinimum.GetHashCode() ^ mMaximum.GetHashCode();
    }

    private readonly float mMinimum;
    private readonly float mMaximum;
  }
}
