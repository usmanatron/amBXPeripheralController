namespace aPC.Chromesthesia.Pitch
{
  public class Pitch
  {
    public float lowerFrequency;
    public float upperFrequency;
    public float amplitude;
    public int fftBinIndex;

    public Pitch(int fftBinIndex, float fftBinSize, float amplitude)
    {
      this.lowerFrequency = fftBinIndex * fftBinSize;
      this.upperFrequency = (fftBinIndex + 1) * fftBinSize;
      this.amplitude = amplitude;
      this.fftBinIndex = fftBinIndex;
    }

    public float averageFrequency
    {
      get { return (lowerFrequency + upperFrequency) / 2; }
    }
  }
}