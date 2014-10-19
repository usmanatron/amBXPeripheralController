using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Engine;
using aPC.Common.Server.SceneHandlers;
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

      int sampleRate = 44100;

      var streamRaw = new WaveInProvider(waveIn);
      var streamPitch = new PitchGeneratorProvider(streamRaw, new FftPitchDetector(sampleRate), new FftPitchDetector(sampleRate), new FloatDataStereoSplitter());
      var compositeLightSectionBuilder = new SceneBuilder(new CompositeLightSectionBuilder(new LightSectionBuilder(), new CompositeLightBuilder()), new LightBuilder());

      var frameConductor = new FrameConductor(new FrameActor(new EngineManager()), new FrameHandler(new SceneAccessor().GetScene("rainbow"), EventComplete));
      var streamScene = new SceneGeneratorProvider(streamPitch, compositeLightSectionBuilder, new ConductorManager(frameConductor));
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

    /// <summary>
    /// The action to take when an event has been run
    /// </summary>
    /// <remarks>
    ///   As events are not supported, we never expect this to be called, therefore throw an exception!
    /// </remarks>
    private static void EventComplete()
    {
      throw new InvalidOperationException("Attempted to run an event through the Chromesthesia application - this should never happen!");
    }
  }
}
