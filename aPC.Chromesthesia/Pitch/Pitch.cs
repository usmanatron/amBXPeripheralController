namespace aPC.Chromesthesia.Pitch
{
  public class Pitch
  {
    public float lowerFrequency;
    public float upperFrequency;
    public float amplitude;

    public Pitch(float lowerFrequency, float upperFrequency, float amplitude)
    {
      this.lowerFrequency = lowerFrequency;
      this.upperFrequency = upperFrequency;
      this.amplitude = amplitude;
    }

    public float averageFrequency
    {
      get
      {
        return (lowerFrequency + upperFrequency) / 2;
      }
    }
  }
}
