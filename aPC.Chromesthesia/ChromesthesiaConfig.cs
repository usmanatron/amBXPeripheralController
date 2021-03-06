﻿using System;
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
    public static int InputAudioSampleRate => 44100;

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
    public static int InputBufferLengthPerSample => 16384;

    /// <summary>
    /// The number of float samples in each sample per channel (i.e. left and right speaker)
    /// </summary>
    /// <remarks>
    ///
    /// </remarks>
    public static int InputBufferFloatSamplesPerChannel => InputBufferLengthPerSample / 8;

    #region FFT

    public static int FftLowerDetectionFrequency => int.Parse(config["FftLowerDetectionFrequency"]);

    public static int FftUpperDetectionFrequency => int.Parse(config["FftUpperDetectionFrequency"]);

    /// <summary>
    /// The interval which the PitchResult Summary Writer employs when there is no data to write
    /// </summary>
    /// <remarks>
    /// If this value is changed without proper consideration, it may cause memory overflows
    /// (too high) or excessive cpu usage (too low).
    /// The chosen value appears to be a safe option, given a few tests.  Change with caution!</remarks>
    public static int PitchSummaryWriterSleepInterval => 30;

    #region Calculated Values

    /// <summary>
    /// The width of each FFT bin
    /// </summary>
    public static float FFTBinSize => InputAudioSampleRate / InputBufferFloatSamplesPerChannel;

    public static int FFTMinimumBinSize => (int)(FftLowerDetectionFrequency / FFTBinSize);

    public static int FFTMaximumBinSize => (int)(FftUpperDetectionFrequency / FFTBinSize);

    #endregion Calculated Values

    #endregion FFT

    #region Lights

    /// <summary>
    /// When building a diagonal light, we use a percentage of the left and right lights.
    /// This value defines what percentage of the closer side we use when building.
    /// Expected to be between 50 and 100 (enforced elsewhere).
    /// </summary>
    public static int DiagonalLightPercentageOfSide => int.Parse(config["DiagonalLightPercentageOfSide"]);

    public static int FrameLength => int.Parse(config["FrameLength"]);

    public static int LightFadeTime => int.Parse(config["LightSectionFadeTime"]);

    public static int LightComponentMultiplicationFactor => int.Parse(config["LightComponentMultiplicationFactor"]);

    /// <summary>
    /// The maximum number of FFT samples to use when constructing the lights, ordered by amplitue (descending)
    /// For example, a value of 5 means the five FFT bins with highest amplitude are only used.
    /// </summary>
    /// <remarks>
    /// A value <= 0 implies that all results should be used
    /// </remarks>
    public static int LightMaximumSamplesToUse => int.Parse(config["LightMaximumSamplesToUse"]);

    /// <summary>
    /// Set to true if the Normal CDF-based Colour builder should be used.
    /// A value of anything except "true" will cause the LightBuilder to fall back to using Colour Traingles
    /// </summary>
    public static bool LightBuilderUsesNormalCDF => bool.Parse(config["LightBuilderUsesNormalCDF"]);

    /// <summary>
    /// The ...MainFrequencyRange objects define the main range of frequencies which the given colour component applies
    /// For example, a RedMFR of 100-200 implies that the main range for red is between 100Hz and 200Hz.  The IColourBuilder
    /// class in effect uses this to appropriately translate the sound sample into the right hue.
    /// </summary>
    /// <remarks>
    /// Note that this does *not* necessarily mean that red will only be shown within this range - merely that the
    /// strongest red will be here.  This is particularly true for the Normal CDF IColourBuilder, which bleeds through this range!
    /// </remarks>
    public static Tuple<int, int> RedMainFrequencyRange => GetFrequencyRange(config["RedMainFrequencyRange"]);

    public static Tuple<int, int> GreenMainFrequencyRange => GetFrequencyRange(config["GreenMainFrequencyRange"]);

    public static Tuple<int, int> BlueMainFrequencyRange => GetFrequencyRange(config["BlueMainFrequencyRange"]);

    private static Tuple<int, int> GetFrequencyRange(string input)
    {
      var range = input.Split(':');
      return new Tuple<int, int>(int.Parse(range[0]), int.Parse(range[1]));
    }

    #endregion Lights
  }
}