using System;

namespace aPC.Client.Disco
{
  /// <summary>
  /// A random number generator whose width can be specified.
  /// </summary>
  class CustomScaleRandomNumberGenerator
  {
    public CustomScaleRandomNumberGenerator(float xiMinimum, float xiMaximum)
    {
      mGenerator = new Random();

      Minimum = xiMinimum;
      Maximum = xiMaximum;
    }

    public float GetNext
    {
      get
      {
        return ((float)mGenerator.NextDouble() * Width) + Minimum;
      }
    }

    private float Width
    {
      get
      {
        return Maximum - Minimum;
      }
    }

    private float Minimum;
    private float Maximum;
    private Random mGenerator;
  }
}
