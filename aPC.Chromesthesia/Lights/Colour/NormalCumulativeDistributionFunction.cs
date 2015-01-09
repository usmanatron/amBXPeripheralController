using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia.Lights.Colour
{
  /// <summary>
  /// Implements the Normal Cumulative Distribution Function, as defined here: http://en.wikipedia.org/wiki/Normal_distribution#Cumulative_distribution_function.
  /// </summary>
  /// <remarks>
  /// Absolute accuracy is not important here (speed is much more important), therefore, we use
  /// the "integration by parts" decomposition and only compute the first few terms.
  /// </remarks>
  internal class NormalCumulativeDistributionFunction
  {
    private readonly int mean;
    private readonly int standardDeviation;

    //TODO: Do some research to determine if this truly is the maximum we should allow!
    private const int maximumNumberOfCDFTerms = 200;

    public NormalCumulativeDistributionFunction(int mean, int standardDeviation)
    {
      if (ChromesthesiaConfig.NormalCDFNumberOfTerms > maximumNumberOfCDFTerms)
      {
        var message = string.Format("The config demands that we calculate more terms in the CDF function than is wise (max is {0}).  Aborting!",
          maximumNumberOfCDFTerms);
        throw new ArgumentException(message);
      }

      this.mean = mean;
      this.standardDeviation = standardDeviation;
    }

    public float ComputeCDF(float value)
    {
      var normalisedPoint = (value - mean) / standardDeviation;
      var exponential = Math.Exp(-0.5 * normalisedPoint * normalisedPoint);

      var sequenceValue = 0d;

      for (int term = 0; term < ChromesthesiaConfig.NormalCDFNumberOfTerms; term++)
      {
        var power = (2 * term) + 1;
        var denominator = GetDoubleFactorial(power);
        sequenceValue += Math.Pow(normalisedPoint, power) / denominator;
      }

      return 0.5f + (float)(sequenceValue * exponential) / (float)Math.Sqrt(2 * Math.PI);
    }

    /// <summary>
    /// Defined here: http://en.wikipedia.org/wiki/Double_factorial
    /// Assumes the input is positive!
    /// </summary>
    private double GetDoubleFactorial(int input)
    {
      var value = 1;
      for (int term = input; term > 1; term = term - 2)
      {
        value *= term;
      }

      return value;
    }
  }
}