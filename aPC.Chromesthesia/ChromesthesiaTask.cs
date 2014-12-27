using System;
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

    public void Execute(bool runForever)
    {
      int bufferSize = ChromesthesiaConfig.InputBufferLengthPerSample;

      if (bufferSize % 8 != 0)
      {
        throw new ArgumentException("Input buffer size must be disible by 8.  Given value is: " + bufferSize);
      }

      do
      {
        sceneGenerator.Execute(bufferSize);
      } while (runForever);
    }
  }
}