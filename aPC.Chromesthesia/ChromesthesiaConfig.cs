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

    #endregion Lights
  }
}