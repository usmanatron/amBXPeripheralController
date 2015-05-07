using aPC.Chromesthesia.Logging;
using aPC.Chromesthesia.Server;
using aPC.Chromesthesia.Sound;
using aPC.Common.Client.Communication;
using NAudio.Wave;
using Ninject;
using System;

namespace aPC.Chromesthesia
{
  internal class Chromesthesia
  {
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
      var notifiationClient = kernel.Get<NotificationClientBase>();
      var streamScene = new SceneGenerator(streamPitch, compositeLightSectionBuilder, notifiationClient);

      var task = new ChromesthesiaTask(streamScene);

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
      Console.WriteLine(preRunSummary, waveFormat.BitsPerSample, waveFormat.AverageBytesPerSecond, waveFormat.Channels, waveFormat.SampleRate, waveFormat.Encoding);
    }
  }
}