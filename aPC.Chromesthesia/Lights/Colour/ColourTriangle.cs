using System;

namespace aPC.Chromesthesia.Lights.Colour
{
  /// <summary>
  /// Handles the value of a colour.
  /// For a given start and end index, values between 0 and 1 are calculated so that,
  /// if graphed, you get a triangle, (with midPoint being the highest point).
  /// </summary>
  public class ColourTriangle : MidpointRadiusColourBuilder
  {
    public ColourTriangle(Tuple<int, int> frequencyRange)
      : base(frequencyRange)
    {
    }

    public override float GetValue(float frequency)
    {
      var scaledFrequency = (frequency - midPoint + radius) / radius;

      var zeroCentredValue = scaledFrequency - 1;

      if (IsNotInRange(zeroCentredValue))
      {
        return 0f;
      }

      // Divide by 2 to ensure that the values returned are not too large (white-out)
      return (1 - Math.Abs(zeroCentredValue)) / 2;
    }

    /// <remarks>
    /// This parity is used to allow for short-circuiting in most (if not all!) scenarios
    /// </remarks>
    private bool IsNotInRange(float value)
    {
      return Math.Abs(value) > 1f;
    }
  }
}