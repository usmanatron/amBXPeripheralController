using NAudio.Dsp;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Chromesthesia.Pitch
{
  /* qqUMI TODO:
   * Increase buffer to 8192 if possible (decrease bin size)
   * Use NAudio to calculate FFT (instead of some random library)
   */

  // FFT based pitch detector. seems to work best with block sizes of 4096
  public class FftPitchDetector : IPitchDetector
  {
    private float sampleRate;

    public FftPitchDetector(float sampleRate)
    {
      this.sampleRate = sampleRate;
    }

    private float[] fftBuffer;
    private Complex[] fftBufferComplex;
    private float[] prevBuffer;
    private Complex[] prevBufferComplex;

    public float DetectPitch(float[] buffer, int inFrames)
    {
      if (prevBuffer == null)
      {
        prevBuffer = new float[inFrames];
        prevBufferComplex = new Complex[inFrames];// / 2];
      }

      // double frames since we are combining present and previous buffers
      int frames = inFrames * 2;
      if (fftBuffer == null)
      {
        fftBuffer = new float[frames * 2]; // times 2 because it is complex input
        fftBufferComplex = new Complex[frames];
      }

      for (int n = 0; n < frames; n++)
      {
        if (n < inFrames)
        {

          fftBuffer[n * 2] = prevBuffer[n] * (float) FastFourierTransform.HammingWindow(n, frames);
          fftBufferComplex[n].X = prevBufferComplex[n].X * (float)FastFourierTransform.HammingWindow(n, frames);
          
          fftBuffer[n * 2 + 1] = 0; // need to clear out as fft modifies buffer
          fftBufferComplex[n].Y = 0;
        }
        else
        {
          fftBuffer[n * 2] = buffer[n - inFrames] * (float) FastFourierTransform.HammingWindow(n, frames);
          fftBufferComplex[n].X = buffer[n - inFrames] * (float) FastFourierTransform.HammingWindow(n, frames);
          
          fftBuffer[n * 2 + 1] = 0; // need to clear out as fft modifies buffer
          fftBufferComplex[n].Y = 0;
        }
      }

      // assuming frames is a power of 2
      SmbPitchShift.smbFft(fftBuffer, frames, -1);
      FastFourierTransform.FFT(true, fftBufferComplex.Length /*11*/, fftBufferComplex);

      float binSize = sampleRate / frames;
      int minBin = (int)(60 / binSize);
      int maxBin = (int)(600 / binSize);
      float maxIntensity = 0f;
      float maxIntensityC = 0f;
      int maxBinIndex = 0;
      int maxBinIndexC = 0;
      for (int bin = minBin; bin <= maxBin; bin++)
      {
        float real = fftBuffer[bin * 2];
        float realC = fftBufferComplex[bin].X;

        float imaginary = fftBuffer[bin * 2 + 1];
        float imaginaryC = fftBufferComplex[bin].Y;

        float intensity = real * real + imaginary * imaginary;
        float intensityC = realC * realC * imaginaryC * imaginaryC;

        if (intensity > maxIntensity)
        {
          maxIntensity = intensity;
          maxBinIndex = bin;
        }
        if (intensityC > maxIntensityC)
        {
          maxIntensityC = intensityC;
          maxBinIndexC = bin;
        }

      }
      if (maxBinIndex != maxBinIndexC) maxBinIndexC++;

      return binSize * maxBinIndex;
    }
  }
}
