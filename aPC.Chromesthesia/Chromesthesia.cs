using aPC.Chromesthesia.Lights;
using aPC.Chromesthesia.Server;
using aPC.Chromesthesia.Sound;
using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Server;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Entities;
using NAudio.Wave;
using Ninject;
using System;

namespace aPC.Chromesthesia
{
  internal class Chromesthesia
  {
    private static ChromesthesiaTask task;
    private const string preRunSummary = @"Bits per sample: {0}
Average bytes per second: {1}
Channels: {2}
Sample rate: {3}
Encoding: {4}";

    private static void Main()
    {
      var kernel = new NinjectKernelHandler().Kernel;
      var waveIn = kernel.Get<WasapiLoopbackCapture>();
      waveIn.StartRecording();

      WriteCaptureSettings(waveIn.WaveFormat);

      var streamRaw = new WaveInProvider(waveIn);

      var streamPitch = new PitchGeneratorProvider(streamRaw, new FftPitchDetector(), new FftPitchDetector(), new FloatDataStereoSplitter(), new PitchResultSummaryWriter());
      var compositeLightSectionBuilder = kernel.Get<SceneBuilder>();

      var sceneRunner = kernel.Get<SceneRunner>();
      var streamScene = new SceneGenerator(streamPitch, compositeLightSectionBuilder, sceneRunner);

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
      Console.WriteLine(string.Format(preRunSummary, waveFormat.BitsPerSample, waveFormat.AverageBytesPerSecond, waveFormat.Channels, waveFormat.SampleRate, waveFormat.Encoding));
    }
  }
}