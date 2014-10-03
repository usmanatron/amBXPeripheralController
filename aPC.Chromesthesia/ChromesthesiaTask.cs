using System.Threading;

namespace aPC.Chromesthesia
{
  // The following link goes through understanding FFT and getting frequencies from it:
  // http://channel9.msdn.com/coding4fun/articles/AutotuneNET
  class ChromesthesiaTask
  {
    private readonly SceneGeneratorProvider sceneGenerator;

    public ChromesthesiaTask(SceneGeneratorProvider sceneGenerator)
    {
      this.sceneGenerator = sceneGenerator;
    }

    public void Run()
    {
      ApplyAutoTune(true);

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
    public void ApplyAutoTune(bool runForever)
    {
      byte[] buffer = new byte[8192];
      int bytesRead;

      do
      {
        //sceneGenerator.TestServer();
        bytesRead = sceneGenerator.Read(buffer, 0, buffer.Length);
        //Thread.Sleep(100);
      } while (runForever);//bytesRead != 0);
    }
  }
}