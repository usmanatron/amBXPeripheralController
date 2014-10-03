namespace aPC.Chromesthesia.Pitch
{
  public interface IPitchDetector
  {
    PitchResult DetectPitchDistribution(float[] buffer, int frames);
  }
}
