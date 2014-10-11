using System.Collections.Generic;
using NAudio.Dsp;
using System;
using System.Linq;
using NAudio.Wave;

namespace aPC.Chromesthesia.Pitch
{
  public class FftPitchDetector : IPitchDetector
  {
    private readonly float sampleRate;
    private Complex[] fftBuffer;
    private CircularWaveBuffer previousBuffer;

    // Settings
    private const int lowerDetectionFrequency = 60;
    private const int upperDetectionFrequency = 600;

    public FftPitchDetector(float sampleRate)
    {
      this.sampleRate = sampleRate;
    }

    public PitchResult Result
    {
      get { return result; }
    }

    private PitchResult result;

    /// <remarks>
    /// This algorithm uses previous data in order to have a larger amount of data
    /// to calculate against (which in turn gives a more accurate value.
    /// In this case, the previous 3 buffers are stored.
    /// This method is void to allow us to use Threads.
    /// </remarks>
    public void DetectPitchDistribution(WaveBuffer buffer, int inFrames)
    {
      // 4 times the number of frames input since we are combining present and 3 previous buffers
      int frames = inFrames * 4;

      SetupBuffers(buffer, inFrames, frames);

      BuildFFTDataToTransform(buffer.FloatBuffer, inFrames, frames);

      var power = (int) Math.Log(frames, 2);
      FastFourierTransform.FFT(true, power, fftBuffer);

      var pitchResult = BuildResults(fftBuffer, frames);
      UpdatePreviousBuffer(buffer);
      this.result = pitchResult;
    }

    private void SetupBuffers(byte[] buffer, int inFrames, int frames)
    {
      if (previousBuffer == null)
      {
        previousBuffer = new CircularWaveBuffer(3, inFrames);
      }

      if (fftBuffer == null)
      {
        fftBuffer = new Complex[frames];
      }
    }

    private void BuildFFTDataToTransform(float[] buffer, int inFrames, int frames)
    {
      /* qqUMI CURRENTLY DOESN'T WORK
       * Doesn't like using WaveReader for some reason.  Instead of getting a length of ~3000 we're getting ~12000
       * Particularly strange since, when we had buffer = float[] it knew it had length 1024.  Need to find out why that example works 
       * so I can fix this one here properly.
       *
       * Alternative potentially is to simplify it in some way?  Maybe go back to storing just the byte array and manually convert to
       * floats?  A royal pain but can fix properly later.
       */

      float[] prevBuffers = previousBuffer.GetFloatBuffer().ToArray();
      var previousBufferCount = 3 * inFrames;

      for (int n = 0; n < frames; n++)
      {
        if (n < previousBufferCount) //qqUMI
        {
          fftBuffer[n].X = prevBuffers[n] * (float)FastFourierTransform.HammingWindow(n, frames);
          fftBuffer[n].Y = 0;
        }
        else
        {
          fftBuffer[n].X = buffer[n - previousBufferCount] * (float)FastFourierTransform.HammingWindow(n, frames);
          fftBuffer[n].Y = 0;
        }
      }
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

    private void UpdatePreviousBuffer(WaveBuffer buffer)
    {
      previousBuffer.AddChunk(buffer);
    }
  }
}
