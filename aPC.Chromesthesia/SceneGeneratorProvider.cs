using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia
{
  class SceneGeneratorProvider : IWaveProvider
  {
    private IWaveProvider sourceProvider;
    private WaveBuffer waveBuffer;
    private int release;
    private int maxHold;
    private float previousPitch;
    private PitchDetector pitchDetector;

    public SceneGeneratorProvider(PitchDetector pitchDetector, IWaveProvider sourceProvider)
    {
      if (sourceProvider.WaveFormat.SampleRate != 44100)
      {
        throw new ArgumentException("AutoTune only works at 44.1kHz");
      }
      if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
      {
        throw new ArgumentException("AutoTune only works on IEEE floating point audio data");
      }

      this.sourceProvider = sourceProvider;
      this.waveBuffer = new WaveBuffer(8192);
      this.pitchDetector = pitchDetector;
    }

    public int Read(byte[] buffer, int offset, int count)
    {

      if (waveBuffer == null || waveBuffer.MaxSize < count)
      {
        waveBuffer = new WaveBuffer(count);
      }

      int bytesRead = sourceProvider.Read(waveBuffer, 0, count);

      // the last bit sometimes needs to be rounded up:
      if (bytesRead > 0) bytesRead = count;

      int frames = bytesRead / sizeof(float);
      float pitch = pitchDetector.DetectPitch(waveBuffer.FloatBuffer, frames);

      // an attempt to make it less "warbly" by holding onto the pitch 
      // for at least one more buffer
      if (pitch == 0f && release < maxHold)
      {
        pitch = previousPitch;
        release++;
      }
      else
      {
        this.previousPitch = pitch;
        release = 0;
      }

      Console.WriteLine(pitch); //Debugging qqUMI
      WaveBuffer outBuffer = new WaveBuffer(buffer);
      return frames * 4;
    }

    public WaveFormat WaveFormat
    {
      get { throw new NotImplementedException(); }
    }
  }
}
