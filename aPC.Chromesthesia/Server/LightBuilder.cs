using aPC.Chromesthesia.Pitch;
using aPC.Common.Entities;
using System.Linq;

namespace aPC.Chromesthesia.Server
{
  class LightBuilder
  {
    public Light BuildLightFrom(PitchResult pitchResult)
    {
      var light = new Light
                  {
                    Red = 0f,
                    Blue = 0f,
                    Green = 0f,
                    Intensity = 0.8f
                  };

      var spectrumWidth = pitchResult.Pitches.Count;

      var red = new ColourCurve(-1 * (spectrumWidth / 2),(spectrumWidth / 2) /*- 1*/);
      var green = new ColourCurve(3 + 1 /* qqUMI +1*/, spectrumWidth - 2);
      var blue = new ColourCurve((spectrumWidth / 2) /*+ 1*/, (3 * spectrumWidth / 2));

      foreach (var pitch in pitchResult.Pitches.OrderBy(p => p.fftBinIndex))
      {
        light.Red += red.GetValue(pitch.fftBinIndex) * pitch.amplitude * MultFactor / spectrumWidth;
        light.Blue += blue.GetValue(pitch.fftBinIndex) * pitch.amplitude * MultFactor / spectrumWidth;
        light.Green += green.GetValue(pitch.fftBinIndex) * pitch.amplitude * MultFactor / spectrumWidth;
      }
      
      return light;
    }

    private const int MultFactor = 100;
  }
}
