using aPC.Chromesthesia.Pitch;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia
{
  class Chromesthesia
  {
    static void Main(string[] args)
    {
      var waveIn = new WasapiLoopbackCapture();
      waveIn.StartRecording();

      OutputCaptureSettings(waveIn);
      
      var streamRaw = new WaveInProvider(waveIn);
      var streamEffect = new SceneGeneratorProvider(streamRaw, new PitchDetector(), new FloatDataStereoSplitter());
      var task = new ChromesthesiaTask(streamEffect);

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
