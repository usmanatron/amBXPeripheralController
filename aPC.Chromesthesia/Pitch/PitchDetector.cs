namespace aPC.Chromesthesia.Pitch
{
  class PitchDetector
  {
    private readonly FftPitchDetector fftPitchDetector;

    public PitchDetector()
    {
      int sampleRate = 44100;
      fftPitchDetector = new FftPitchDetector(sampleRate);
    }

    public PitchResult DetectPitch(float[] buffer, int frames)
    {
      var pitchResult = fftPitchDetector.DetectPitchDistribution(buffer, frames);
      return pitchResult;
    }
  }
}