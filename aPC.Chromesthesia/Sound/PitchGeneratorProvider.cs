using aPC.Chromesthesia.Sound.Entities;
using NAudio.Wave;
using System;

namespace aPC.Chromesthesia.Sound
{
  internal class PitchGeneratorProvider : IWaveProvider
  {
    private readonly IWaveProvider sourceProvider;
    private readonly IPitchDetector leftPitchDetector;
    private readonly IPitchDetector rightPitchDetector;
    private readonly FloatDataStereoSplitter stereoSplitter;
    private readonly PitchResultSummaryWriter resultWriter;

    private WaveBuffer intermediaryBuffer;
    private WaveBuffer leftBuffer;
    private WaveBuffer rightBuffer;

    public StereoPitchResult PitchResults { get; private set; }

    public PitchGeneratorProvider(IWaveProvider sourceProvider, IPitchDetector leftPitchDetector, IPitchDetector rightPitchDetector, FloatDataStereoSplitter stereoSplitter, PitchResultSummaryWriter resultWriter)
    {
      if (sourceProvider.WaveFormat.SampleRate != ChromesthesiaConfig.InputAudioSampleRate)
      {
        throw new ArgumentException("This provider only works at 44.1kHz");
      }
      if (sourceProvider.WaveFormat.Encoding != WaveFormatEncoding.IeeeFloat)
      {
        throw new ArgumentException("This provider only works on IEEE floating point audio data");
      }

      this.sourceProvider = sourceProvider;
      this.stereoSplitter = stereoSplitter;
      this.resultWriter = resultWriter;

      this.leftPitchDetector = leftPitchDetector;
      this.rightPitchDetector = rightPitchDetector;
    }

    public int Read(byte[] buffer, int offset, int count)
    {
      SetupWaveBuffers(count);
      int bytesRead = FillIntermediateBufferAndReturnBytesRead(count);

      var stereoFrames = FillStereoBuffersAndReturnFrames();

      AnalysePitch(stereoFrames, bytesRead);

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

    private void AnalysePitch(int stereoFrames, int bytesRead)
    {
      var leftPitchResult = leftPitchDetector.DetectPitchDistribution(leftBuffer.FloatBuffer, stereoFrames);
      var rightPitchResult = rightPitchDetector.DetectPitchDistribution(rightBuffer.FloatBuffer, stereoFrames);

      resultWriter.Enqueue(leftPitchResult, rightPitchResult);
      PitchResults = new StereoPitchResult(leftPitchResult, rightPitchResult, bytesRead);
    }

    public WaveFormat WaveFormat
    {
      get { return sourceProvider.WaveFormat; }
    }
  }
}