using System;

namespace aPC.Common.Server
{
  // an infinite ticker that deals with an atypical first run (e.g. the first run has more steps,
  // which are dropped in subsequent runs)
  public class AtypicalFirstRunInfiniteTicker
  {
    public AtypicalFirstRunInfiniteTicker(int initialCount, int subsequentCount)
    {
      // It's fine to have a scene with no repeatble frames
      if (initialCount <= 0 || subsequentCount < 0)
      {
        var error = string.Format("Attempted to create a ticker with non-positive inputs: {0}, {1}",
          initialCount,
          subsequentCount);
        throw new InvalidOperationException(error);
      }

      this.initialCount = initialCount;
      this.subsequentCount = subsequentCount;

      Index = 0;
      IsFirstRun = true;
    }

    public void Advance()
    {
      Index++;
      var lCount = IsFirstRun ? initialCount : subsequentCount;

      if (Index == lCount)
      {
        IsFirstRun = false;
        Index = 0;
      }
    }

    public void Refresh()
    {
      Index = 0;
      IsFirstRun = true;
    }

    public int Index { get; private set; }

    public bool IsFirstRun { get; private set; }

    private readonly int initialCount;
    private readonly int subsequentCount;
  }
}