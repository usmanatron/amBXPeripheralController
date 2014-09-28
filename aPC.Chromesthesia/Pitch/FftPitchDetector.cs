using NAudio.Dsp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Pitch
{
  /* qqUMI TODO:
   * Increase buffer to 8192 if possible (decrease bin size)
   */

  // FFT based pitch detector. seems to work best with block sizes of 4096
  public class FftPitchDetector : IPitchDetector
  {
    private float sampleRate;
    private Complex[] fftBufferComplex;
    private Complex[] prevBufferComplex;

    public FftPitchDetector(float sampleRate)
    {
      this.sampleRate = sampleRate;
    }

    public float DetectPitch(float[] buffer, int inFrames)
    {
      if (prevBufferComplex == null)
      {
        prevBufferComplex = new Complex[inFrames];// / 2];
      }

      // double frames since we are combining present and previous buffers
      int frames = inFrames * 2;
      if (fftBufferComplex == null)
      {
        fftBufferComplex = new Complex[frames];
      }

      for (int n = 0; n < frames; n++)
      {
        if (n < inFrames)
        {
          fftBufferComplex[n].X = GetIntensity(prevBufferComplex[n]) * (float)FastFourierTransform.HammingWindow(n, frames);
          fftBufferComplex[n].Y = 0;
        }
        else
        {
          fftBufferComplex[n].X = buffer[n - inFrames] * (float) FastFourierTransform.HammingWindow(n, frames);
          fftBufferComplex[n].Y = 0;
        }
      }

      var power = (int) Math.Log(frames, 2);
      FastFourierTransform.FFT(true, power, fftBufferComplex);

      float binSize = sampleRate / frames;
      int minBin = (int)(60 / binSize);
      int maxBin = (int)(600 / binSize);
      float maxIntensity = 0f;
      int maxBinIndex = 0;
      for (int bin = minBin; bin <= maxBin; bin++)
      {
        float real = fftBufferComplex[bin].X;
        float imaginary = fftBufferComplex[bin].Y;
        float intensity = real * real * imaginary * imaginary;

        if (intensity > maxIntensity)
        {
          maxIntensity = intensity;
          maxBinIndex = bin;
        }
      }

      return binSize * maxBinIndex;
    }

    private float GetIntensity(Complex value)
    {
      return (float)Math.Sqrt((double)(value.X * value.X * value.Y * value.Y));
    }
  }
}
