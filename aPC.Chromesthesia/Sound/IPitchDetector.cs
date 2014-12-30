using aPC.Chromesthesia.Sound.Entities;

namespace aPC.Chromesthesia.Sound
{
  public interface IPitchDetector
  {
    PitchResult DetectPitchDistribution(float[] buffer, int frames);
  }
}