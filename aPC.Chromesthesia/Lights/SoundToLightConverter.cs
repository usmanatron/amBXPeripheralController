using aPC.Chromesthesia.Sound.Entities;
using aPC.Common.Entities;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Lights
{
  internal class SoundToLightConverter
  {
    private readonly LightBuilder builder;

    public SoundToLightConverter(LightBuilder builder)
    {
      this.builder = builder;
    }

    public Light BuildLightFrom(PitchResult pitchResult)
    {
      builder.Reset();
      if (pitchResult.TotalAmplitude <= 0.01f) //TODO Add a configurable threshold
      {
        return builder.Build();
      }

      foreach (var pitch in GetPitchesUnderConsideration(pitchResult))
      {
        builder.AddPitch(pitch, pitchResult.TotalAmplitude);
      }

      return builder.Build();
    }

    private IEnumerable<Pitch> GetPitchesUnderConsideration(PitchResult pitchResult)
    {
      return ChromesthesiaConfig.LightMaximumSamplesToUse <= 0
        ? pitchResult.Pitches
        : pitchResult.Pitches
            .OrderByDescending(pitch => pitch.amplitude)
            .Take(ChromesthesiaConfig.LightMaximumSamplesToUse);
    }
  }
}