using aPC.Chromesthesia.Sound.Entities;
using System;

namespace aPC.Chromesthesia.Logging
{
  internal class PitchResultSummary
  {
    public PitchResult leftResult;
    public PitchResult rightResult;
    public TimeSpan time;

    public PitchResultSummary(PitchResult leftResult, PitchResult rightResult)
    {
      this.leftResult = leftResult;
      this.rightResult = rightResult;
      time = DateTime.Now.TimeOfDay;
    }
  }
}