using System.Collections.Specialized;
using System.Configuration;

namespace aPC.Chromesthesia
{
  /// <summary>
  /// Contains settings vales which are used in a variety of places. Not all of
  /// these are configurable; in cases where they are hard-coded the reasoning is given.
  /// </summary>
  public static class ChromesthesiaConfig
  {
    private static readonly NameValueCollection config;

    static ChromesthesiaConfig()
    {
      config = ConfigurationManager.GetSection("ChromesthesiaConfig") as NameValueCollection;
    }

    /// <summary>
    /// We assume the input sample rate is 44.1KHz.  This is required
    /// in the PitchGenerator.
    /// </summary>
    /// <remarks>
    /// The code assumes this to be the case in a couple of places; hence it isn't
    /// a good idea to change this yet!
    /// </remarks>
    public static int InputAudioSampleRate
    {
      get
      {
        return 44100;
      }
    }

    /// <summary>
    /// The size of the buffer to fill for each sample.
    /// ** Must be divisible by 8 ** (this allows us to convert to floats (/4) and split to stereo (/2)).
    /// ** Must be a power to 2 ** (to allow the later FFT step to work nicely)
    /// </summary>
    /// <remarks>
    /// Make the buffer too long and pitches aren't detected fast enough!
    /// Potentially good buffer sizes are 8192, 4096, 2048, 1024; however further
    /// experimentaion is needed for the optimal value.  As such, changing this value
    /// should be done with care.
    /// </remarks>
    public static int InputBufferLengthPerSample
    {
      get
      {
        return 8192;
      }
    }

    /// <summary>
    /// The number of float samples in each sample per channel (i.e. left and right speaker)
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    public static int InputBufferFloatSamplesPerChannel
    {
      get
      {
        return InputBufferLengthPerSample / 8;
      }
    }

    #region FFT

    public static int FftLowerDetectionFrequency
    {
      get { return int.Parse(config["FftLowerDetectionFrequency"]); }
    }

    public static int FftUpperDetectionFrequency
    {
      get { return int.Parse(config["FftUpperDetectionFrequency"]); }
    }

    /// <summary>
    /// The interval which the PitchResult Summary Writer employs when there is no data to write
    /// </summary>
    /// <remarks>
    /// If this value is changed without proper consideration, it may cause memory overflows
    /// (too high) or excessive cpu usage (too low).
    /// The chosen value appears to be a safe option, given a few tests.  Change with caution!</remarks>
    public static int PitchSummaryWriterSleepInterval
    {
      get { return 30; }
    }

    #region Calculated Values

    /// <summary>
    /// The width of each FFT bin
    /// </summary>
    public static float FFTBinSize
    {
      get
      {
        return InputAudioSampleRate / InputBufferFloatSamplesPerChannel;
      }
    }

    public static int FFTMinimumBinSize
    {
      get
      {
        return (int)(FftLowerDetectionFrequency / FFTBinSize);
      }
    }

    public static int FFTMaximumBinSize
    {
      get
      {
        return (int)(FftUpperDetectionFrequency / FFTBinSize);
      }
    }

    #endregion Calculated Values

    #endregion FFT

    #region Lights

    /// <summary>
    /// When building a diagonal light, we use a percentage of the left and right lights.
    /// This value defines what percentage of the closer side we use when building.
    /// Expected to be between 50 and 100 (enforced elsewhere).
    /// </summary>
    public static int DiagonalLightPercentageOfSide
    {
      get { return int.Parse(config["DiagonalLightPercentageOfSide"]); }
    }

    public static int SceneFrameLength
    {
      get { return int.Parse(config["SceneFrameLength"]); }
    }

    public static int LightFadeTime
    {
      get { return int.Parse(config["LightSectionFadeTime"]); }
    }

    public static int LightComponentMultiplicationFactor
    {
      get { return int.Parse(config["LightComponentMultiplicationFactor"]); }
    }

    /// <summary>
    /// The maximum number of FFT samples to use when constructing the lights, ordered by amplitue (descending)
    /// For example, a value of 5 means the five FFT bins with highest amplitude are only used.
    /// </summary>
    /// <remarks>
    /// A value <= 0 implies that all results should be used
    /// </remarks>
    public static int LightMaximumSamplesToUse
    {
      get { return int.Parse(config["LightMaximumSamplesToUse"]); }
    }

    /// <summary>
    /// Set to true if the Normal CDF-based Colour builder should be used.
    /// A value of anything except "true" will cause the LightBuilder to fall back to using Colour Traingles
    /// </summary>
    public static bool LightBuilderUsesNormalCDF
    {
      get { return bool.Parse(config["LightBuilderUsesNormalCDF"]); }
    }

    /// <summary>
    /// The number of terms to calculate for the Normal Cumulative Distribution Function (CDF).
    /// The more terms, the higher the accuracy.  However this is a trade-off between accuracy and performance.
    /// </summary>
    public static int NormalCDFNumberOfTerms
    {
      get { return int.Parse(config["NormalCDFNumberOfTerms"]); }
    }

    #endregion Lights
  }
}