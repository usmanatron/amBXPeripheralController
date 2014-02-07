using System.Threading;

namespace aPC.Client.Disco
{
  public class Settings
  {
    public Settings()
    {
      SetConfigurableDefaultValues();
      SetConstantValues();
    }

    private void SetConfigurableDefaultValues()
    {
      BPM = 150;
      RedColourWidth = new Range(0, 1);
      BlueColourWidth = new Range(0, 1);
      GreenColourWidth = new Range(0, 1);
      FanWidth = new Range(0, 0);
      LightIntensityWidth = new Range(0, 1);
    }

    private void SetConstantValues()
    {
      ChangeThreshold = 0.5d;
      FadeTime = 10;
    }

    public int PushInterval
    {
      get
      {
        return (1000 * 60) / BPM;
      }
    }

    public int BPM { private get; set; }
    public Range RedColourWidth;
    public Range BlueColourWidth;
    public Range GreenColourWidth;
    public Range LightIntensityWidth;
    public Range FanWidth;

    public double ChangeThreshold;
    public int FadeTime;
  }
}
