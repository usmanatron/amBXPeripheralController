using System;

namespace aPC.Chromesthesia.Lights.Colour
{
  /// <summary>
  /// An implementation of ColourBuilder which uses midpoint and radius values to calculate the colour value
  /// </summary>
  internal abstract class MidpointRadiusColourBuilder : IColourBuilder
  {
    protected int midPoint;
    protected int radius;

    protected MidpointRadiusColourBuilder(Tuple<int, int> frequencyRange)
    {
      midPoint = GetMidpoint(frequencyRange);
      radius = GetMidpointOfDifference(frequencyRange.Item2);
    }

    /// <remarks>
    /// Values are expected to be in ascending order!
    /// Decimal values are rounded down to integers
    /// </remarks>
    private int GetMidpoint(Tuple<int, int> frequencyRange)
    {
      if (frequencyRange.Item1 > frequencyRange.Item2)
      {
        throw new ArgumentException("Values given are not in ascending order!");
      }

      var frequencyMidpoint = frequencyRange.Item1 + ((frequencyRange.Item1 + frequencyRange.Item2) / 2);
      return (int)Math.Floor(frequencyMidpoint / ChromesthesiaConfig.FFTBinSize);
    }

    /// <remarks>
    /// Decimal values are rounded ***UP*** to the nearest integer
    /// </remarks>
    private int GetMidpointOfDifference(int maximumFrequency)
    {
      var scaledMaximumFrequency = maximumFrequency / ChromesthesiaConfig.FFTBinSize;
      var absoluteDifference = Math.Abs(scaledMaximumFrequency - midPoint);

      return (int)Math.Ceiling(absoluteDifference / 2d);
    }

    public abstract float GetValue(int index);
  }
}