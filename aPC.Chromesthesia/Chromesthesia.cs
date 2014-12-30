using aPC.Chromesthesia.Pitch;
using aPC.Chromesthesia.Server;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Engine;
using aPC.Common.Server.SceneHandlers;
using NAudio.Wave;
using Ninject;
using System;

namespace aPC.Chromesthesia
{
  internal class Chromesthesia
  {
    private static ChromesthesiaTask task;

    private static void Main()
    {
      var kernel = new NinjectKernelHandler().Kernel;
      var waveIn = kernel.Get<WasapiLoopbackCapture>();
      waveIn.StartRecording();

      WriteCaptureSettings(waveIn.WaveFormat);

      var streamRaw = new WaveInProvider(waveIn);

      var streamPitch = new PitchGeneratorProvider(streamRaw, new FftPitchDetector(), new FftPitchDetector(), new FloatDataStereoSplitter(), new PitchResultSummaryWriter());
      var compositeLightSectionBuilder = new SceneBuilder(new CompositeLightSectionBuilder(new LightSectionBuilder(), new CompositeLightBuilder()), new SoundToLightConverter(new LightBuilder()));

      var frameConductor = new FrameConductor(new FrameActor(new EngineManager()), new FrameHandler(new SceneAccessor(new DefaultScenes()).GetScene("rainbow"), EventComplete));
      var streamScene = new SceneGenerator(streamPitch, compositeLightSectionBuilder, new ConductorManager(frameConductor));

      task = new ChromesthesiaTask(streamScene);

      task.Run();
    }

    /// <remarks>
    ///   Expected values are as follows.  It's not necessarily a problem if these differ:
    ///     Bits per sample: 32
    ///     Average bytes per second: 352800
    ///     Channels: 2
    ///     Sample rate: 44100
    ///     Encoding: IeeeFloat
    /// </remarks>
    private static void WriteCaptureSettings(WaveFormat waveFormat)
    {
      Console.WriteLine("Bits per sample: " + waveFormat.BitsPerSample);
      Console.WriteLine("Average bytes per second: " + waveFormat.AverageBytesPerSecond);
      Console.WriteLine("Channels: " + waveFormat.Channels);
      Console.WriteLine("Sample rate: " + waveFormat.SampleRate);
      Console.WriteLine("Encoding: " + waveFormat.Encoding);
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