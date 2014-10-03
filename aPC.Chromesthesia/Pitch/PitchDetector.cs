namespace aPC.Chromesthesia.Pitch
{
  class PitchDetector
  {
    private FftPitchDetector fftPitchDetector;

    private readonly int maxHold;
    private int release;
    private float previousPitch;

    public PitchDetector()
    {
      int sampleRate = 44100;
      fftPitchDetector = new FftPitchDetector(sampleRate);

      maxHold = 2;
    }

    public float DetectPitch(float[] buffer, int frames)
    {
      var pitchResult = fftPitchDetector.DetectPitchDistribution(buffer, frames);

      var pitch = pitchResult.PeakPitch.lowerFrequency;
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