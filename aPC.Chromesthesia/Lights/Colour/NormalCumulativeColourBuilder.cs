using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace aPC.Chromesthesia.Lights.Colour
{
  internal class NormalCumulativeColourBuilder : MidpointRadiusColourBuilder
  {
    private NormalCumulativeDistributionFunction normalCDF;

    public NormalCumulativeColourBuilder(Tuple<int, int> frequencyRange)
      : base(frequencyRange)
    {
      // In this case, the radium needs to halved (to ensure the values returned are not too large)
      radius = (int)Math.Ceiling(radius / 2d);

      normalCDF = new NormalCumulativeDistributionFunction(midPoint, radius);
    }

    public override float GetValue(float frequency)
    {
      var rawValue = normalCDF.ComputeCDF(frequency);

      return rawValue <= 0.5
        ? rawValue
        : 1 - rawValue;
    }
  }
}