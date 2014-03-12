using System;

namespace aPC.Common.Server
{
  // an infinite ticker that deals with an atypical first run (e.g. the first run has more steps, 
  // which are dropped in subsequent runs)
  public class AtypicalFirstRunInfiniteTicker
  {
    public AtypicalFirstRunInfiniteTicker(int xiInitialCount, int xiSubsequentCount)
    {
      // It's fine to have a scene with no repeatble frames
      if (xiInitialCount <= 0 || xiSubsequentCount < 0)
      {
        var lError = string.Format("Attempted to create a ticker with non-positive inputs: {0}, {1}", 
          xiInitialCount,
          xiSubsequentCount);
        throw new InvalidOperationException(lError);
      }

      mInitialCount = xiInitialCount;
      mSubsequentCount = xiSubsequentCount;

      Index = 0;
      IsFirstRun = true;
    }

    public void Advance()
    {
      Index++;
      var lCount = IsFirstRun ? mInitialCount : mSubsequentCount;

      if (Index == lCount)
      {
        IsFirstRun = false;
        Index = 0;
      }
    }

    public int Index { get; private set; }
    public bool IsFirstRun { get; private set; }

    private readonly int mInitialCount;
    private readonly int mSubsequentCount;

  }
}
