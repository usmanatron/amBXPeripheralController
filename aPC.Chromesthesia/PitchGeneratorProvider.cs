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

    private void AnalysePitch(int stereoFrames, int bytesRead)
    {
      var leftPitchResult  = leftPitchDetector.DetectPitchDistribution(leftBuffer.FloatBuffer, stereoFrames);
      var rightPitchResult = rightPitchDetector.DetectPitchDistribution(rightBuffer.FloatBuffer, stereoFrames);

      WriteToConsole(leftPitchResult, rightPitchResult); //Debugging qqUMI
      PitchResults = new StereoPitchResult(leftPitchResult, rightPitchResult, bytesRead);
    }

    private void WriteToConsole(PitchResult leftResult, PitchResult rightResult)
    {
      var leftPeakPitchNonZero  = ToNonZeroString(leftResult.PeakPitch.averageFrequency);
      var rightPeakPitchNonZero = ToNonZeroString(rightResult.PeakPitch.averageFrequency);
      var leftMaxPitchAmp  = ToNonZeroString(leftResult.PeakPitch.amplitude);
      var rightMaxPitchAmp = ToNonZeroString(rightResult.PeakPitch.amplitude);


      if (leftPeakPitchNonZero != "       " || rightPeakPitchNonZero != "       ")
      {
        Console.WriteLine("{0} | {1}", leftPeakPitchNonZero, rightPeakPitchNonZero);
      }

      if (leftMaxPitchAmp != "       " || rightMaxPitchAmp != "       ")
      {
        Console.WriteLine("{0} | {1}", leftMaxPitchAmp, rightMaxPitchAmp);
      }
    }

    private string ToNonZeroString(float value)
    {
      return value == 0 ? "       " : value.ToString("000.000"); 
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
