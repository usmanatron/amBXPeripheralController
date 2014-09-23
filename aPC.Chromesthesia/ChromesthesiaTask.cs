// The following link goes through understanding FFT and getting frequencies from it:
// http://channel9.msdn.com/coding4fun/articles/AutotuneNET

using NAudio.Wave;
using System;
using System.Threading;

namespace aPC.Chromesthesia
{
  class ChromesthesiaTask
  {

    private IWaveIn waveIn;


    public void Run()
    {

      waveIn = new WasapiLoopbackCapture();
      //waveIn.DataAvailable += OnDataAvailable;
      waveIn.StartRecording();

      Console.WriteLine(waveIn.WaveFormat.BitsPerSample); //32
      Console.WriteLine(waveIn.WaveFormat.AverageBytesPerSecond); //352800
      Console.WriteLine(waveIn.WaveFormat.Channels); // 2
      Console.WriteLine(waveIn.WaveFormat.SampleRate); // 44100
      Console.WriteLine(waveIn.WaveFormat.Encoding); // IeeeFloat

      ApplyAutoTune();

      while (true)
      {
        Thread.Sleep(10 * 1000);
      }

    }
    public void ApplyAutoTune()
    {
      var streamRaw = new WaveInProvider(waveIn);

      //var stream32 = new Wave16ToFloatProvider(streamRaw);
      var streamEffect = new SceneGeneratorProvider(new PitchDetector(), streamRaw);

      // buffer length needs to be a power of 2 for FFT to work nicely
      // however, make the buffer too long and pitches aren't detected fast enough
      // successful buffer sizes: 8192, 4096, 2048, 1024
      // (some pitch detection algorithms need at least 2048)
      byte[] buffer = new byte[8192];
      int bytesRead;

      do
      {
        bytesRead = streamEffect.Read(buffer, 0, buffer.Length);
      } while (true);//bytesRead != 0);
    }


    private void OnDataAvailable(object sender, WaveInEventArgs e)
    {
      var buffer = e.Buffer;
      int bytesRecorded = e.BytesRecorded;

      for (var index = 0; index < bytesRecorded; index += 2)
      {
        var sample = ((buffer[index + 1] << 8) | buffer[index]);
        var normalisedSample = (sample - 32768) / 32768f; 
        var sample32bit = sample / 32768f;


        //sampleAggregator.Add(sample32bit);
      }
    }
  }
}









