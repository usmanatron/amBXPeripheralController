namespace aPC.Chromesthesia
{
  /// <summary>
  /// Contains settings vales which are used in a variety of places
  /// </summary>
  public static class Settings
  {
    /// <summary>
    /// We assume the input sample rate is 44.1KHz.  This is required
    /// in the PitchGenerator.
    /// </summary>
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
      get
      {
        return 100;
      }
    }

    public static int FftUpperDetectionFrequency
    {
      get
      {
        return 1000;
      }
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
      get
      {
        return 70;
      }
    }

    public static int SceneFrameLength
    {
      get
      {
        return 10;
      }
    }

    public static int LightSectionFadeTime
    {
      get
      {
        return 2;
      }
    }

    public static int LightComponentMultiplicationFactor
    {
      get
      {
        return 60;
      }
    }

    #endregion Lights
  }
}