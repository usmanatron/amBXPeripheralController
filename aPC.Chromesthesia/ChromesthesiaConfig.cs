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

    public static int LightSectionFadeTime
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

    #endregion Lights
  }
}