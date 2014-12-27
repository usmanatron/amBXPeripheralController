using NAudio.Dsp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Pitch
{
  /// <summary>
  /// Pitch detector based on Fast Fourier Transform (FFT).
  /// </summary>
  public class FftPitchDetector : IPitchDetector
  {
    private Complex[] fftBuffer;
    private float[] prevBuffer;

    public PitchResult DetectPitchDistribution(float[] buffer, int inFrames)
    {
      if (prevBuffer == null)
      {
        prevBuffer = new float[buffer.Count()];
      }

      // double frames since we are combining present and previous buffers
      int frames = inFrames * 2;
      if (fftBuffer == null)
      {
        fftBuffer = new Complex[frames];
      }

      for (int n = 0; n < frames; n++)
      {
        if (n < inFrames)
        {
          fftBuffer[n].X = prevBuffer[n] * (float)FastFourierTransform.HammingWindow(n, frames);
          fftBuffer[n].Y = 0;
        }
        else
        {
          fftBuffer[n].X = buffer[n - inFrames] * (float)FastFourierTransform.HammingWindow(n, frames);
          fftBuffer[n].Y = 0;
        }
      }

      var power = (int)Math.Log(frames, 2);
      FastFourierTransform.FFT(true, power, fftBuffer);

      var result = BuildResults(fftBuffer, frames);
      UpdatePreviousBuffer(buffer);
      return result;
    }

    private PitchResult BuildResults(Complex[] fftBuffer, int frames)
    {
      var pitches = new List<Pitch>();

      for (int bin = ChromesthesiaConfig.FFTMinimumBinSize; bin <= ChromesthesiaConfig.FFTMaximumBinSize; bin++)
      {
        pitches.Add(new Pitch(bin, ChromesthesiaConfig.FFTBinSize, GetIntensity(fftBuffer[bin])));
      }

      return new PitchResult(pitches);
    }

    private float GetIntensity(Complex value)
    {
      return (float)Math.Sqrt((double)((value.X * value.X) + (value.Y * value.Y)));
    }

    private void UpdatePreviousBuffer(float[] fftBuffer)
    {
      prevBuffer = fftBuffer;
    }
  }
}