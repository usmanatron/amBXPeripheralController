// The following link goes through understanding FFT and getting frequencies from it:
// http://channel9.msdn.com/coding4fun/articles/AutotuneNET

using NAudio.Wave;
using System;
using System.Threading;

namespace aPC.Chromesthesia
{
  class ChromesthesiaTask
  {
    private SceneGeneratorProvider stream;

    public ChromesthesiaTask(SceneGeneratorProvider stream)
    {
      this.stream = stream;
    }

    public void Run()
    {
      ApplyAutoTune();

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
    public void ApplyAutoTune()
    {
      byte[] buffer = new byte[8192];
      int bytesRead;

      do
      {
        bytesRead = stream.Read(buffer, 0, buffer.Length);
      } while (true);//bytesRead != 0);
    }
  }
}