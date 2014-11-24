using aPC.Chromesthesia.Pitch;
using aPC.Common.Entities;
using System.Linq;

namespace aPC.Chromesthesia.Server
{
  internal class LightBuilder
  {
    private readonly int componentMultiplicationFactor;
    private readonly int amplitudeMultiplicationFactor;

    public LightBuilder()
    {
      this.componentMultiplicationFactor = Settings.LightComponentMultiplicationFactor;
      this.amplitudeMultiplicationFactor = Settings.LightIntensityMultiplicationFactor;
    }

    public Light BuildLightFrom(PitchResult pitchResult)
    {
      var light = GetEmptyLight();
      var spectrumWidth = pitchResult.Pitches.Count;

      // These are magic numbers and may need tweaking to get the colour scheme absolutely right
      var red = new ColourTriangle(0, spectrumWidth / 4);
      var green = new ColourTriangle(spectrumWidth / 2, spectrumWidth / 3);
      var blue = new ColourTriangle((3 * spectrumWidth / 4), spectrumWidth / 2);

      foreach (var pitch in pitchResult.Pitches.OrderBy(p => p.fftBinIndex))
      {
        var amplitudePercentage = pitch.amplitude / pitchResult.TotalAmplitude;

        light.Red += red.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor / spectrumWidth;
        light.Green += green.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor / spectrumWidth;
        light.Blue += blue.GetValue(pitch.fftBinIndex) * amplitudePercentage * componentMultiplicationFactor / spectrumWidth;
      }
      light.Intensity = pitchResult.TotalAmplitude * amplitudeMultiplicationFactor;

      return light;
    }

    private Light GetEmptyLight()
    {
      return new Light
      {
        Red = 0f,
        Blue = 0f,
        Green = 0f,
        Intensity = 0f,
        FadeTime = 10
      };
    }
  }
}