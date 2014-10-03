using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server;
using aPC.Common;
using aPC.Common.Server.Engine;
using NAudio.Wave;
using System;

namespace aPC.Chromesthesia
{
  class Chromesthesia
  {
    private static ChromesthesiaTask task;

    static void Main()
    {
      var kernel = new NinjectKernelHandler();
      var waveIn = kernel.Get<WasapiLoopbackCapture>();
      waveIn.StartRecording();

      OutputCaptureSettings(waveIn);
      


      var streamRaw = new WaveInProvider(waveIn);
      var streamPitch = new PitchGeneratorProvider(streamRaw, new PitchDetector(), new PitchDetector(), new FloatDataStereoSplitter());
      var streamScene = new SceneGeneratorProvider(streamPitch, new SceneBuilder(), new ConductorManager(new EngineManager(), new SceneAccessor()));
      task = new ChromesthesiaTask(streamScene);

      task.Run();
    }

    private static void OutputCaptureSettings(IWaveIn waveIn)
    {
      Console.WriteLine(waveIn.WaveFormat.BitsPerSample); //32
      Console.WriteLine(waveIn.WaveFormat.AverageBytesPerSecond); //352800
      Console.WriteLine(waveIn.WaveFormat.Channels); // 2
      Console.WriteLine(waveIn.WaveFormat.SampleRate); // 44100
      Console.WriteLine(waveIn.WaveFormat.Encoding); // IeeeFloat
    }
  }
}
