using aPC.Common.Entities;
using System;

namespace aPC.Server
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

    public AtypicalFirstRunInfiniteTicker(amBXScene scene)
      : this(scene.Frames.Count, scene.RepeatableFrames.Count)
    {
    }

    public AtypicalFirstRunInfiniteTicker(int initialCount, int subsequentCount)
    {
      ResetTicker(initialCount, subsequentCount);
    }

    public void Reset(int newInitialCount, int newSubsequentCount)
    {
      ResetTicker(newInitialCount, newSubsequentCount);
    }

    private void ResetTicker(int newInitialCount, int newSubsequentCount)
    {
      // It's fine to have a subsequentCount of zero (this denotes a single run).
      if (newInitialCount <= 0 || newSubsequentCount < 0)
      {
        var error = string.Format("Attempted to create a ticker with non-positive inputs: {0}, {1}",
          newInitialCount,
          newSubsequentCount);
        throw new InvalidOperationException(error);
      }

      initialCount = newInitialCount;
      subsequentCount = newSubsequentCount;
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