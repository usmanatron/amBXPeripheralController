namespace aPC.Client.Disco
{
  public class Settings
  {
    public Settings()
    {
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

        var lMaximumFadeLength = PushInterval / (FramesPerScene - 1);
        FadeTime = new Range(10, lMaximumFadeLength);
      }
    }

    private int mBPM;

    public Range RedColourWidth;
    public Range BlueColourWidth;
    public Range GreenColourWidth;
    public Range LightIntensityWidth;
    public Range FadeTime;

    public double ChangeThreshold;
    public int FramesPerScene;
  }
}
