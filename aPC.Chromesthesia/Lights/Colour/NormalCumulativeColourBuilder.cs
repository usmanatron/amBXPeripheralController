using MathNet.Numerics.Distributions;
using System;

namespace aPC.Chromesthesia.Lights.Colour
{
  /// <remarks>
  /// * midPoint refers to the mean
  /// * radius refers to the standard deviation
  /// </remarks>
  internal class NormalCumulativeColourBuilder : MidpointRadiusColourBuilder
  {
    public NormalCumulativeColourBuilder(Tuple<int, int> frequencyRange)
      : base(frequencyRange)
    {
    }

    public override float GetValue(int index)
    {
      var rawValue = (float)Normal.CDF(midPoint, radius, index);

      return rawValue <= 0.5
        ? rawValue
        : 1 - rawValue;
    }
  }
}