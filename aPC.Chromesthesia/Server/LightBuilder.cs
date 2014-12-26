using aPC.Chromesthesia.Pitch;
using aPC.Common.Entities;
using System.Collections.Generic;
using System.Linq;
using Pitch = aPC.Chromesthesia.Pitch.Pitch;

namespace aPC.Chromesthesia.Server
{
  internal class LightBuilder
  {
    private readonly int componentMultiplicationFactor;
    private readonly int maximumSamplesUnderConsideration;

    public LightBuilder()
    {
      this.componentMultiplicationFactor = ChromesthesiaConfig.LightComponentMultiplicationFactor;
      this.maximumSamplesUnderConsideration = ChromesthesiaConfig.LightMaximumSamplesToUse;
    }

    public Light BuildLightFrom(PitchResult pitchResult)
    {
      var light = GetEmptyLight();
      //TODO: The value of spectrumWidth shouln't (I claim) ever change when running - this would allow for simplification!
      var spectrumWidth = pitchResult.Pitches.Count;

      // These are magic numbers and may need tweaking to get the colour scheme absolutely right
      var red = new ColourTriangle(0, spectrumWidth / 4);
      var green = new ColourTriangle(spectrumWidth / 2, spectrumWidth / 3);
      var blue = new ColourTriangle((3 * spectrumWidth / 4), spectrumWidth / 2);

      foreach (var pitch in GetPitchesUnderConsideration(pitchResult))
      {
        var amplitudePercentage = pitch.amplitude / pitchResult.TotalAmplitude;

        light.Red += red.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor / spectrumWidth;
        light.Green += green.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor / spectrumWidth;
        light.Blue += blue.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor / spectrumWidth;
      }

      return light;
    }

    private IEnumerable<Pitch.Pitch> GetPitchesUnderConsideration(PitchResult pitchResult)
    {
      return maximumSamplesUnderConsideration <= 0
        ? pitchResult.Pitches
        : pitchResult.Pitches
            .OrderByDescending(pitch => pitch.amplitude)
            .Take(maximumSamplesUnderConsideration);
    }

    private Light GetEmptyLight()
    {
      return new Light
      {
        Red = 0f,
        Blue = 0f,
        Green = 0f,
        FadeTime = 10
      };
    }
  }
}