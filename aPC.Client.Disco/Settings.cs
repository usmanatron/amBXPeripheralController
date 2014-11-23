using aPC.Common.Client;

namespace aPC.Client.Disco
{
  public class Settings
  {
    private int mBPM;

    public Range RedColourWidth;
    public Range BlueColourWidth;
    public Range GreenColourWidth;
    public Range LightIntensityWidth;
    public Range FadeTime;

    public double ChangeThreshold;
    public int FramesPerScene;

    public HostnameAccessor HostnameAccessor;

    public Settings(HostnameAccessor hostnameAccessor)
    {
      HostnameAccessor = hostnameAccessor;
      SetConstantValues();
      SetConfigurableDefaultValues();
    }

    private void SetConfigurableDefaultValues()
    {
      BPM = 150;
      RedColourWidth = new Range(0, 1);
      BlueColourWidth = new Range(0, 1);
      GreenColourWidth = new Range(0, 1);
      LightIntensityWidth = new Range(0, 1);
    }

    private void SetConstantValues()
    {
      ChangeThreshold = 0.5d;
      FramesPerScene = 4;
    }

    public int PushInterval
    {
      get
      {
        return (1000 * 60) / BPM;
      }
    }

    public int BPM
    {
      private get
      {
        return mBPM;
      }
      set
      {
        mBPM = value;

        var maximumFadeLength = PushInterval / (FramesPerScene - 1);
        FadeTime = new Range(10, maximumFadeLength);
      }
    }
  }
}