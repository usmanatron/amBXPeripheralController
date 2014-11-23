using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Pitch
{
  public class PitchResult
  {
    public List<Pitch> Pitches { get; private set; }

    public float TotalAmplitude { get; private set; }

    public Pitch PeakPitch { get; private set; }

    public PitchResult(List<Pitch> pitches)
    {
      Pitches = pitches;
      TotalAmplitude = Pitches.Sum(pitch => pitch.amplitude);
      PeakPitch = GetPeakPitch();
    }

    private Pitch GetPeakPitch()
    {
      // If they're all the same amplitude, we assume they're all empty => do nothing
      if (Pitches.Max(pitch => pitch.amplitude) == Pitches.Min(pitch => pitch.amplitude))
      {
        return new Pitch(0, 0, 0);
      }

      var currentPeakPitch = Pitches.Aggregate(
        (pitch1, pitch2) => pitch1.amplitude > pitch2.amplitude
          ? pitch1
          : pitch2);

      return currentPeakPitch;
    }
  }
}