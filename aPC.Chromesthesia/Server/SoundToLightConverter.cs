using aPC.Chromesthesia.Pitch;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Pitch = aPC.Chromesthesia.Pitch.Pitch;

namespace aPC.Chromesthesia.Server
{
  internal class SoundToLightConverter
  {
    private LightBuilder builder;

    public SoundToLightConverter(LightBuilder builder)
    {
      this.builder = builder;
    }

    public Light BuildLightFrom(PitchResult pitchResult)
    {
      builder.Reset();
      var componentMultiplicationFactor = ChromesthesiaConfig.LightComponentMultiplicationFactor;

      foreach (var pitch in GetPitchesUnderConsideration(pitchResult))
      {
        builder.AddPitch(pitch, pitchResult.TotalAmplitude);
      }

      return builder.Build();
    }

    private IEnumerable<Pitch.Pitch> GetPitchesUnderConsideration(PitchResult pitchResult)
    {
      return ChromesthesiaConfig.LightMaximumSamplesToUse <= 0
        ? pitchResult.Pitches
        : pitchResult.Pitches
            .OrderByDescending(pitch => pitch.amplitude)
            .Take(ChromesthesiaConfig.LightMaximumSamplesToUse);
    }
  }
}