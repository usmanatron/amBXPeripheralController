using aPC.Chromesthesia.Pitch;
using NAudio.Wave;
using System;

namespace aPC.Chromesthesia
{
  class PitchGeneratorProvider : IWaveProvider
  {
    private readonly IWaveProvider sourceProvider;
    private readonly IPitchDetector leftPitchDetector;
    private readonly IPitchDetector rightPitchDetector;
    private readonly FloatDataStereoSplitter stereoSplitter;
    
    private WaveBuffer intermediaryBuffer;
    private WaveBuffer leftBuffer;
    private WaveBuffer rightBuffer;

    public StereoPitchResult PitchResults { get; private set; }

    public PitchGeneratorProvider(IWaveProvider sourceProvider, IPitchDetector leftPitchDetector, IPitchDetector rightPitchDetector, FloatDataStereoSplitter stereoSplitter)
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
      var leftPitchResult  = leftPitchDetector.DetectPitchDistribution(leftBuffer.FloatBuffer, stereoFrames);
      var rightPitchResult = rightPitchDetector.DetectPitchDistribution(rightBuffer.FloatBuffer, stereoFrames);

      WriteSummaryToConsole(leftPitchResult, rightPitchResult);
      PitchResults = new StereoPitchResult(leftPitchResult, rightPitchResult, bytesRead);
    }

    private void WriteSummaryToConsole(PitchResult leftResult, PitchResult rightResult)
    {
      var leftPeakPitchNonZero  = ToNonZeroString(leftResult.PeakPitch.averageFrequency, "0000.000");
      var rightPeakPitchNonZero = ToNonZeroString(rightResult.PeakPitch.averageFrequency, "0000.000");
      var leftTotalAmp  = ToNonZeroString(leftResult.TotalAmplitude, "0.00000");
      var rightTotalAmp = ToNonZeroString(rightResult.TotalAmplitude, "0.00000");


      if (leftPeakPitchNonZero != "       " || rightPeakPitchNonZero != "       " || leftTotalAmp != "       " || rightTotalAmp != "       ")
      {
        Console.WriteLine("{0} : {1} <- PP | MA -> {2} : {3}", leftPeakPitchNonZero, rightPeakPitchNonZero, leftTotalAmp, rightTotalAmp);
      }
    }

    private string ToNonZeroString(float value, string format)
    {
      return value == 0 ? "       " : value.ToString(format); 
    }

    public WaveFormat WaveFormat
    {
      get { return sourceProvider.WaveFormat; }
    }
  }
}
