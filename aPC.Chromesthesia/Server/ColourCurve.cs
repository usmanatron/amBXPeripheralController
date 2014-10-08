using System;

namespace aPC.Chromesthesia.Server
{
  /// <summary>
  /// Handles the curve of a colour.
  /// For a given start and end index, values between 0 and 1 are calculated so that,
  /// if graphed, you get a bell curve.
  /// </summary>
  class ColourCurve
  {
    private readonly int startIndex;
    private readonly int endIndex;

    public ColourCurve(int startIndex, int endIndex)
    {
      if ((startIndex + endIndex) % 2 != 0)
      {
        throw new ArgumentException("Both indexes must have the same parity!");
      }

      if (startIndex >= endIndex)
      {
        throw new ArgumentException("Start index must be less than the ending index.");
      }

      this.startIndex = startIndex;
      this.endIndex = endIndex;
    }

    public float GetValue(int index)
    {
      if (!IsInRange(index))
      {
        return 0f;
      }

      if (index > midpoint)
      {
        return (endIndex - index) / midpointToEndDistance;
      }
      else
      {
        return (index - startIndex) / midpointToEndDistance;
      }
    }

    private bool IsInRange(int index)
    {
      return startIndex <= index &&
             index <= endIndex;
    }

    private int midpoint
    {
      get
      {
        return (startIndex + endIndex) / 2;
      }
    }

    private float midpointToEndDistance
    {
      get { return endIndex - midpoint; }
    }

  }
}
