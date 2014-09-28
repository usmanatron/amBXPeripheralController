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
    private PitchDetector pitchDetector;    
    private FloatDataStereoSplitter stereoSplitter;
    
    private WaveBuffer intermediaryBuffer;
    private WaveBuffer leftBuffer;
    private WaveBuffer rightBuffer;

    public SceneGeneratorProvider(IWaveProvider sourceProvider, PitchDetector pitchDetector, FloatDataStereoSplitter stereoSplitter)
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
      this.stereoSplitter = stereoSplitter;

      this.pitchDetector = pitchDetector;
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      SetupWaveBuffers(count);

      int bytesRead = sourceProvider.Read(intermediaryBuffer, 0, count);

      // the last bit sometimes needs to be rounded up:
      if (bytesRead > 0) bytesRead = count;
      
      //qqUMI
      var stereoFrames = FillStereoBuffersAndReturnFrames();
      var leftPitch  = pitchDetector.DetectPitch(leftBuffer.FloatBuffer, stereoFrames);
      var rightPitch = pitchDetector.DetectPitch(rightBuffer.FloatBuffer, stereoFrames);

      if (leftPitch != 0 || rightPitch != 0)
      {
        Console.WriteLine("{0} | {1}", leftPitch.ToString(), rightPitch.ToString()); //Debugging qqUMI
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

    private int FillStereoBuffersAndReturnFrames()
    {
      var stereoBuffer = stereoSplitter.Split(intermediaryBuffer);

      leftBuffer.BindTo(stereoBuffer.LeftChannel);
      rightBuffer.BindTo(stereoBuffer.RightChannel);
      return stereoBuffer.frames;
    }

    public WaveFormat WaveFormat
    {
      get { throw new NotImplementedException(); }
    }
  }
}
