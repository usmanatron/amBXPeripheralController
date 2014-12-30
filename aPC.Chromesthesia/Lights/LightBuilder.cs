using aPC.Chromesthesia.Lights.Colour;
using aPC.Chromesthesia.Sound.Entities;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Lights
{
  internal class LightBuilder
  {
    private IColourBuilder redComponent;
    private IColourBuilder greenComponent;
    private IColourBuilder blueComponent;
    private Light light;

    public LightBuilder()
    {
      var spectrumWidth = ChromesthesiaConfig.FFTMaximumBinSize - ChromesthesiaConfig.FFTMinimumBinSize + 1;
      var oneThirdSpectrumWidth = (int)Math.Floor(spectrumWidth / 3d);

      // Warning: These are magic numbers!
      // TODO: Clean this all up - ideally make it properly configurable through the application config
      if (ChromesthesiaConfig.LightBuilderUsesNormalCDF)
      {
        redComponent = new NormalCumulativeColourBuilder(ChromesthesiaConfig.RedMainFrequencyRange);
        greenComponent = new NormalCumulativeColourBuilder(ChromesthesiaConfig.GreenMainFrequencyRange);
        blueComponent = new NormalCumulativeColourBuilder(ChromesthesiaConfig.BlueMainFrequencyRange);
      }
      else
      {
        redComponent = new ColourTriangle(ChromesthesiaConfig.RedMainFrequencyRange);
        greenComponent = new ColourTriangle(ChromesthesiaConfig.GreenMainFrequencyRange);
        blueComponent = new ColourTriangle(ChromesthesiaConfig.BlueMainFrequencyRange);
      }
    }

    public LightBuilder Reset()
    {
      light = GetEmptyLight();
      return this;
    }

    private Light GetEmptyLight()
    {
      return new Light
      {
        Red = 0f,
        Blue = 0f,
        Green = 0f,
        FadeTime = ChromesthesiaConfig.LightFadeTime
      };
    }

    public LightBuilder AddPitch(Pitch pitch, float totalAmplitude)
    {
      var amplitudePercentage = pitch.amplitude / totalAmplitude;
      light.Red += GetComponentValue(redComponent, pitch.fftBinIndex, amplitudePercentage);
      light.Green += GetComponentValue(greenComponent, pitch.fftBinIndex, amplitudePercentage);
      light.Blue += GetComponentValue(blueComponent, pitch.fftBinIndex, amplitudePercentage);

      return this;
    }

    private Func<IColourBuilder, int, float, float> GetComponentValue = (IColourBuilder colourBuilder, int fftBinIndex, float amplitudePercentage) =>
      {
        return colourBuilder.GetValue(fftBinIndex) * ChromesthesiaConfig.LightComponentMultiplicationFactor * amplitudePercentage;
      };

    public Light Build()
    {
      return light;
    }
  }
}