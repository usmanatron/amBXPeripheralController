using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Pitch
{
  public class PitchResult
  {
    public List<Pitch> Pitches { get; private set; }
    
    public PitchResult(List<Pitch> pitches)
    {
      Pitches = pitches;
    }

    public Pitch PeakPitch
    {
      get
      {
        if (Pitches.Max(pitch => pitch.amplitude) == Pitches.Min(pitch => pitch.amplitude))
        {
          return new Pitch(0,0,0);
        }
        
        return Pitches.Aggregate(
          (pitch1, pitch2) => pitch1.amplitude > pitch2.amplitude 
            ? pitch1 
            : pitch2);
      }
    }
  }
}
