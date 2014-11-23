using System;

namespace aPC.Client.Disco
{
  /// <summary>
  /// Describes a min - max range of values.
  /// </summary>
  public class Range
  {
    private readonly float minimum;
    private readonly float maximum;

    public Range(float minimum, float maximum)
    {
      if (maximum < minimum)
      {
        var message = string.Format(
          "Range constructor values in the wrong order: " + Environment.NewLine +
          "Minimum: {0} " + Environment.NewLine +
          "Maximum: {1}",
          minimum,
          maximum);
        throw new ArgumentException(message);
      }

      this.minimum = minimum;
      this.maximum = maximum;
    }

    /// <remarks>
    ///   value is expected to be between 0 and 1.  If this
    ///   isn't the case, we clip the value appropriately instead of
    ///   throwing any error.
    /// </remarks>
    public float GetScaledValue(double value)
    {
      var clippedValue = CheckValueAndClipIfNecessary(value);

      return ((float)clippedValue * Width) + minimum;
    }

    private double CheckValueAndClipIfNecessary(double value)
    {
      if (value < 0)
      {
        return 0;
      }
      if (1 < value)
      {
        return 1;
      }

      return value;
    }

    public float Width
    {
      get
      {
        return maximum - minimum;
      }
    }

    public override bool Equals(object obj)
    {
      var other = obj as Range;

      if (other == null)
      {
        return false;
      }

      return other.minimum == minimum &&
             other.maximum == maximum;
    }

    public override int GetHashCode()
    {
      return minimum.GetHashCode() ^ maximum.GetHashCode();
    }
  }
}