using System;

namespace aPC.Chromesthesia.Lights.Colour
{
  /// <summary>
  /// Defines an abstract ColourBuilder which uses midpoint and radius values to calculate the colour value
  /// </summary>
  public abstract class MidpointRadiusColourBuilder : IColourBuilder
  {
    protected int midPoint;
    protected int radius;

    public MidpointRadiusColourBuilder(Tuple<int, int> frequencyRange)
    {
      if (frequencyRange.Item1 > frequencyRange.Item2)
      {
        throw new ArgumentException("Values given are not in ascending order!");
      }

      this.midPoint = (frequencyRange.Item1 + frequencyRange.Item2) / 2;
      this.radius = frequencyRange.Item2 - midPoint;
    }

    public abstract float GetValue(float frequency);
  }
}