using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server.Colour;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using Pitch = aPC.Chromesthesia.Pitch.Pitch;

namespace aPC.Chromesthesia.Server
{
  internal class SoundToLightConverter
  {
    private IColourBuilder redComponent;
    private IColourBuilder greenComponent;
    private IColourBuilder blueComponent;
    private Light light;

    public SoundToLightConverter()
    {
      var spectrumWidth = ChromesthesiaConfig.FFTMaximumBinSize - ChromesthesiaConfig.FFTMinimumBinSize + 1;
      var oneThirdSpectrumWidth = (int)Math.Floor(spectrumWidth / 3d);

      // Warning: These are magic numbers!
      // TODO: Clean this all up - ideally make it properly configurable through the application config
      if (ChromesthesiaConfig.LightBuilderUsesNormalCDF)
      {
        redComponent = new NormalCumulativeColourBuilder((int)Math.Floor(spectrumWidth / 6d), (int)Math.Floor((3 * spectrumWidth) / 4d));
        greenComponent = new NormalCumulativeColourBuilder((int)Math.Floor(spectrumWidth / 2d), oneThirdSpectrumWidth);
        blueComponent = new NormalCumulativeColourBuilder((int)Math.Floor(4 * spectrumWidth / 5d), oneThirdSpectrumWidth);
      }
      else
      {
        redComponent = new ColourTriangle(0, spectrumWidth / 4);
        greenComponent = new ColourTriangle(spectrumWidth / 2, spectrumWidth / 3);
        blueComponent = new ColourTriangle((3 * spectrumWidth / 4), spectrumWidth / 2);
      }
    }

    public Light BuildLightFrom(PitchResult pitchResult)
    {
      var light = GetEmptyLight();
      var componentMultiplicationFactor = ChromesthesiaConfig.LightComponentMultiplicationFactor;

      foreach (var pitch in GetPitchesUnderConsideration(pitchResult))
      {
        var amplitudePercentage = pitch.amplitude / pitchResult.TotalAmplitude;

        light.Red += redComponent.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor;
        light.Green += greenComponent.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor;
        light.Blue += blueComponent.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor;
      }

      return light;
    }

    private IEnumerable<Pitch.Pitch> GetPitchesUnderConsideration(PitchResult pitchResult)
    {
      return ChromesthesiaConfig.LightMaximumSamplesToUse <= 0
        ? pitchResult.Pitches
        : pitchResult.Pitches
            .OrderByDescending(pitch => pitch.amplitude)
            .Take(ChromesthesiaConfig.LightMaximumSamplesToUse);
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