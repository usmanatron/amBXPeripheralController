using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia.Pitch
{
  class PitchDetector
  {
    private AutoCorrelator autoCorrelator;
    private FftPitchDetector fftPitchDetector;
    private bool UseAutoCorrelator;

    private int maxHold;
    private int release;
    private float previousPitch;

    public PitchDetector()
    {
      int sampleRate = 44100;
      autoCorrelator = new AutoCorrelator(sampleRate);
      fftPitchDetector = new FftPitchDetector(sampleRate);

      UseAutoCorrelator = false;
      maxHold = 1;
    }

    public float DetectPitch(float[] buffer, int frames)
    {
      float pitch;

      if (UseAutoCorrelator)
      {
        pitch = autoCorrelator.DetectPitch(buffer, frames);
      }
      else
      {
        pitch = fftPitchDetector.DetectPitch(buffer, frames);
      }

      pitch = StabilisePitch(pitch);
      return pitch;
    }

    private float StabilisePitch(float pitch)
    {
      // an attempt to make it less "warbly" by holding onto the pitch 
      // for at least one more buffer
      if (pitch == 0f && release < maxHold)
      {
        pitch = previousPitch;
        release++;
      }
      else
      {
        this.previousPitch = pitch;
        release = 0;
      }

      return pitch;
    }
  }
}