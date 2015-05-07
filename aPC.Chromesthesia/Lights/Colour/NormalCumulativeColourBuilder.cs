using MathNet.Numerics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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
      float rawValue = (float)MathNet.Numerics.Distributions.Normal.CDF(midPoint, radius, index);

      return rawValue <= 0.5
        ? rawValue
        : 1 - rawValue;
    }
  }
}