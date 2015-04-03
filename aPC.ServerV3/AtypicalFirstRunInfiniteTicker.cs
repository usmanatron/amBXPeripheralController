using System;

namespace aPC.ServerV3
{
  /// <summary>
  /// an infinite ticker that deals with an atypical first run (e.g. the first run has more steps,
  /// which are dropped in subsequent runs)
  /// </summary>
  public class AtypicalFirstRunInfiniteTicker
  {
    public int Index { get; private set; }

    public bool IsFirstRun { get; private set; }

    private int initialCount;
    private int subsequentCount;

    public AtypicalFirstRunInfiniteTicker(int initialCount, int subsequentCount)
    {
      ResetTicker(initialCount, subsequentCount);
    }

    public void Reset(int initialCount, int subsequentCount)
    {
      ResetTicker(initialCount, subsequentCount);
    }

    private void ResetTicker(int initialCount, int subsequentCount)
    {
      // It's fine to have a subsequentCount of zero (this denotes a single run).
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
      var count = IsFirstRun ? initialCount : subsequentCount;

      if (Index == count)
      {
        IsFirstRun = false;
        Index = 0;
      }
    }
  }
}