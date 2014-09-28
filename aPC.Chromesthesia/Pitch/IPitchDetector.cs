namespace aPC.Chromesthesia.Pitch
{
  public interface IPitchDetector
  {
    float DetectPitch(float[] buffer, int frames);
  }
}
