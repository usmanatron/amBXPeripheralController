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
      RedGenerator = new CustomScaleRandomNumberGenerator(0, 1);
      BlueGenerator = new CustomScaleRandomNumberGenerator(0, 1);
      GreenGenerator = new CustomScaleRandomNumberGenerator(0, 1);
      FanSpeedGenerator = new CustomScaleRandomNumberGenerator(0, 0);
    }

    public int PushInterval
    {
      get
      {
        return (1000 * 60) / BPM;

      }
    }

    public int BPM { private get; set; }
    public CustomScaleRandomNumberGenerator RedGenerator;
    public CustomScaleRandomNumberGenerator BlueGenerator;
    public CustomScaleRandomNumberGenerator GreenGenerator;
    public CustomScaleRandomNumberGenerator FanSpeedGenerator;
  }
}
