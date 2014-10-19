﻿using System.Collections.Generic;
using NAudio.Dsp;
using System;
using System.Linq;

namespace aPC.Chromesthesia.Pitch
{
  /* TODO: Increase buffer to 8192 if possible (decrease bin size) */
  // 1024 frames are input
  // FFT based pitch detector. seems to work best with block sizes of 4096
  public class FftPitchDetector : IPitchDetector
  {
    private readonly float sampleRate;
    private Complex[] fftBuffer;
    private float[] prevBuffer;

    // Settings
    private const int lowerDetectionFrequency = 60;
    private const int upperDetectionFrequency = 600;

    public FftPitchDetector(float sampleRate)
    {
      this.sampleRate = sampleRate;
    }

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
          fftBuffer[n].X = buffer[n - inFrames] * (float) FastFourierTransform.HammingWindow(n, frames);
          fftBuffer[n].Y = 0;
        }
      }

      var power = (int) Math.Log(frames, 2);
      FastFourierTransform.FFT(true, power, fftBuffer);

      var result = BuildResults(fftBuffer, frames);
      UpdatePreviousBuffer(buffer);
      return result;
    }

    private PitchResult BuildResults(Complex[] fftBuffer, int frames)
    {
      float binSize = sampleRate / frames;
      int minBin = (int)(lowerDetectionFrequency / binSize);
      int maxBin = (int)(upperDetectionFrequency / binSize);

      var pitches = new List<Pitch>();

      for (int bin = minBin; bin <= maxBin; bin++)
      {
        pitches.Add(new Pitch(bin, binSize, GetIntensity(fftBuffer[bin])));
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