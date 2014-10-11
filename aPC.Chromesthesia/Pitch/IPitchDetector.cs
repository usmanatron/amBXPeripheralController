using NAudio.Wave;
namespace aPC.Chromesthesia.Pitch
{
  public interface IPitchDetector
  {
    PitchResult Result { get; }

    void DetectPitchDistribution(WaveBuffer buffer, int frames);
  }
}
