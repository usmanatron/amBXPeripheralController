using System;

namespace aPC.Chromesthesia.Lights.Colour
{
  /// <summary>
  /// Handles the value of a colour.
  /// For a given start and end index, values between 0 and 1 are calculated so that,
  /// if graphed, you get a triangle, (with midPoint being the highest point).
  /// </summary>
  internal class ColourTriangle : MidpointRadiusColourBuilder
  {
    public ColourTriangle(Tuple<int, int> frequencyRange)
      : base(frequencyRange)
    {
    }

    public override float GetValue(int index)
    {
      var value = Math.Abs(index - midPoint) / radius;

      if (IsNotInRange(value))
      {
        return 0f;
      }

      return value;
    }

    /// <remarks>
    /// This parity is used to allow for short-circuiting in most (if not all!) scenarios
    /// </remarks>
    private bool IsNotInRange(float value)
    {
      return value > 1f ||
             value < 0f;
    }
  }
}