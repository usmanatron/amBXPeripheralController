using System.Threading;

namespace aPC.Client.Disco
{
  class Settings
  {
    public Settings()
    {
      SetupDefaultValues();
    }

    private void SetupDefaultValues()
    {
      BPM = 150;
      RedColourWidth = new Range(0, 1);
      BlueColourWidth = new Range(0, 1);
      GreenColourWidth = new Range(0, 1);
      FanWidth = new Range(0, 0);
      LightIntensityWidth = new Range(0, 1);
    }
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
  }
}
