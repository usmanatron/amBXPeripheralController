using aPC.Chromesthesia.Lights.Colour;
using aPC.Chromesthesia.Sound.Entities;
using aPC.Common.Entities;
using System;

namespace aPC.Chromesthesia.Lights
{
  internal class LightBuilder
  {
    private readonly IColourBuilder redComponent;
    private readonly IColourBuilder greenComponent;
    private readonly IColourBuilder blueComponent;
    private Light light;

    public LightBuilder()
    {
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

    private readonly Func<IColourBuilder, int, float, float> GetComponentValue = (colourBuilder, fftBinIndex, amplitudePercentage) =>
      colourBuilder.GetValue(fftBinIndex) * ChromesthesiaConfig.LightComponentMultiplicationFactor * amplitudePercentage;

    public Light Build()
    {
      return light;
    }
  }
}