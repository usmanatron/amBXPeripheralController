using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Chromesthesia
{
  class PitchDetector
  {
    private bool UseAutoCorrelator = true;

    public float DetectPitch(float[] buffer, int frames)
    {
      if (UseAutoCorrelator)
      {
        return new AutoCorrelator(44100).DetectPitch(buffer, frames);
      }
      throw new NotImplementedException();
    }
  }

  class AutoCorrelator
  {
    private float sampleRate;
    private int minOffset;
    private int maxOffset;
    private float[] prevBuffer;


    public AutoCorrelator(int sampleRate)
    {
      this.sampleRate = (float)sampleRate;
      int minFreq = 85;
      int maxFreq = 255;

      this.maxOffset = sampleRate / minFreq;
      this.minOffset = sampleRate / maxFreq;
    }

    public float DetectPitch(float[] buffer, int frames)
    {
      if (prevBuffer == null)
      {
          prevBuffer = new float[frames];
      }
     
      float maxCorr = 0;
      int maxLag = 0;
     
      // starting with low frequencies, working to higher
      for (int lag = maxOffset; lag >= minOffset; lag--)
      {
          float corr = 0; //  sum of squares
          for (int i = 0; i < frames; i++)
          {
              int oldIndex = i - lag;
              float sample = ((oldIndex < 0) ? prevBuffer[frames + oldIndex] : buffer[oldIndex]); 
              corr += (sample * buffer[i]);
          }
          if (corr > maxCorr)
          {
              maxCorr = corr;
              maxLag = lag;
          }
     
      }
      for (int n = 0; n < frames; n++)
      { 
          prevBuffer[n] = buffer[n]; 
      }
      float noiseThreshold = frames / 1000f;
     
      if (maxCorr < noiseThreshold || maxLag == 0) return 0.0f;
      return this.sampleRate / maxLag;
    }
  }
}