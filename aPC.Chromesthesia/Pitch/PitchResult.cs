using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Pitch
{
  public class PitchResult
  {
    public List<Pitch> Pitches { get; private set; }

    private Pitch previousPitch;
    private readonly int maxHold;
    private int release;
    
    public PitchResult(List<Pitch> pitches)
    {
      Pitches = pitches;
      maxHold = 2;
    }

    public Pitch PeakPitch
    {
      get
      {
        // If they're all the same amplitude, we assume they're all empty => do nothing
        if (Pitches.Max(pitch => pitch.amplitude) == Pitches.Min(pitch => pitch.amplitude))
        {
          return new Pitch(0,0,0);
        }

        var currentPeakPitch = Pitches.Aggregate(
          (pitch1, pitch2) => pitch1.amplitude > pitch2.amplitude 
            ? pitch1 
            : pitch2);


        return StabilisePitch(currentPeakPitch);
      }
    }

    // an attempt to make it less "warbly" by holding onto the pitch 
    // for at least one more buffer
    private Pitch StabilisePitch(Pitch currentPeakPitch)
    {
      if (Math.Abs(currentPeakPitch.fftBinIndex - previousPitch.fftBinIndex) == 1 && release < maxHold)
      {
        currentPeakPitch = previousPitch;
        release++;
      }
      else
      {
        this.previousPitch = currentPeakPitch;
        release = 0;
      }

      return currentPeakPitch;
    }
  }
}
