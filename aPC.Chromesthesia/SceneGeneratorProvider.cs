using aPC.Chromesthesia.Pitch;
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
    private PitchDetector leftPitchDetector;
    private PitchDetector rightPitchDetector;
    private FloatDataStereoSplitter stereoSplitter;
    
    private WaveBuffer intermediaryBuffer;
    private WaveBuffer leftBuffer;
    private WaveBuffer rightBuffer;

    public SceneGeneratorProvider(IWaveProvider sourceProvider, PitchDetector leftPitchDetector, PitchDetector rightPitchDetector, FloatDataStereoSplitter stereoSplitter)
    {
      if (sourceProvider.WaveFormat.SampleRate != 44100)
      {
        throw new ArgumentException("This provider only works at 44.1kHz");
      }
      if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
      {
        throw new ArgumentException("This provider only works on IEEE floating point audio data");
      }

      this.sourceProvider = sourceProvider;
      this.stereoSplitter = stereoSplitter;

      this.leftPitchDetector = leftPitchDetector;
      this.rightPitchDetector = rightPitchDetector;
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      SetupWaveBuffers(count);
      int bytesRead = FillIntermediateBufferAndReturnBytesRead(count);
      
      var stereoFrames = FillStereoBuffersAndReturnFrames();
      var leftPitch  = leftPitchDetector.DetectPitch(leftBuffer.FloatBuffer, stereoFrames);
      var rightPitch = rightPitchDetector.DetectPitch(rightBuffer.FloatBuffer, stereoFrames);

      if (leftPitch != 0 || rightPitch != 0)
      {
        Console.WriteLine("{0} | {1}", leftPitch.ToString("000.000"), rightPitch.ToString("000.000")); //Debugging qqUMI
      }
      return bytesRead;
    }

    private void SetupWaveBuffers(int count)
    {

      if (intermediaryBuffer == null || intermediaryBuffer.MaxSize < count)
      {
        intermediaryBuffer = new WaveBuffer(count);

        var singleChannelCount = count / 2;
        leftBuffer = new WaveBuffer(singleChannelCount);
        rightBuffer = new WaveBuffer(singleChannelCount);
      }
    }

    private int FillIntermediateBufferAndReturnBytesRead(int count)
    {
      int bytesRead = sourceProvider.Read(intermediaryBuffer, 0, count);

      // the last bit sometimes needs to be rounded up:
      return bytesRead > 0
        ? count
        : bytesRead;
    }

    private int FillStereoBuffersAndReturnFrames()
    {
      var stereoBuffer = stereoSplitter.Split(intermediaryBuffer);

      leftBuffer.BindTo(stereoBuffer.LeftChannel);
      rightBuffer.BindTo(stereoBuffer.RightChannel);
      return stereoBuffer.frames;
    }

    public WaveFormat WaveFormat
    {
      get { return sourceProvider.WaveFormat; }
    }
  }
}
