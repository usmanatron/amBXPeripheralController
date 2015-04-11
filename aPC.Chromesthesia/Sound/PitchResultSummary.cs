using aPC.Chromesthesia.Sound.Entities;
using System;

namespace aPC.Chromesthesia.Sound
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
      this.time = DateTime.Now.TimeOfDay;
    }
  }
}