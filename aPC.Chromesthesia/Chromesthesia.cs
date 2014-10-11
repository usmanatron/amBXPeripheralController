using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server;
using aPC.Common;
using aPC.Common.Builders;
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
      waveIn.RecordingStopped += RecordingStopped;
      waveIn.StartRecording();

      OutputCaptureSettings(waveIn);

      int sampleRate = 44100;

      var streamRaw = new CircularWaveInProvider(waveIn);
      var streamPitch = new PitchGeneratorProvider(streamRaw, new FftPitchDetector(sampleRate), new FftPitchDetector(sampleRate), new FloatDataStereoSplitter());
      var compositeLightSectionBuilder = new SceneBuilder(new CompositeLightSectionBuilder(new LightSectionBuilder(), new CompositeLightBuilder()), new LightBuilder());
      var streamScene = new SceneGeneratorProvider(streamPitch, compositeLightSectionBuilder, new ConductorManager(new EngineManager(), new SceneAccessor()));
      task = new ChromesthesiaTask(streamScene);

      task.Run();
    }

    private static void RecordingStopped(object sender, StoppedEventArgs e)
    {
      Console.WriteLine(e.Exception);
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
