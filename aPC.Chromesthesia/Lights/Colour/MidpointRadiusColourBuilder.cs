using System;

namespace aPC.Chromesthesia.Lights.Colour
{
  /// <summary>
  /// Defines an abstract Colourbuilder which uses midpoint and radius values to calculate the colour value
  /// </summary>
  internal abstract class MidpointRadiusColourBuilder : IColourBuilder
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
      this.radius = (int)Math.Ceiling(frequencyRange.Item2 - midPoint / 2d);
    }

    public abstract float GetValue(float frequency);
  }
}