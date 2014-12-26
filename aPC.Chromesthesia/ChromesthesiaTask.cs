using System.Threading;

namespace aPC.Chromesthesia
{
  // The following link goes through understanding FFT and getting frequencies from it:
  // http://channel9.msdn.com/coding4fun/articles/AutotuneNET
  internal class ChromesthesiaTask
  {
    private readonly SceneGenerator sceneGenerator;

    public ChromesthesiaTask(SceneGenerator sceneGenerator)
    {
      this.sceneGenerator = sceneGenerator;
    }

    public void Run()
    {
      Execute(true);

      while (true)
      {
        Thread.Sleep(10 * 1000);
      }
    }

    /// <remarks>
    /// buffer length needs to be a power of 2 for FFT to work nicely
    /// however, make the buffer too long and pitches aren't detected fast enough
    /// successful buffer sizes: 8192, 4096, 2048, 1024
    /// (some pitch detection algorithms need at least 2048)
    /// </remarks>
    public void Execute(bool runForever)
    {
      int bufferSize = 8192;

      do
      {
        sceneGenerator.Execute(bufferSize);
      } while (runForever);
    }
  }
}