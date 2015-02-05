using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using System;

namespace aPC.Chromesthesia.Lights
{
  public class CompositeLightSectionBuilder
  {
    private const int centrePercentage = 50;
    private const int sidePercentageMinValue = 50;
    private const int sidePercentageMaxValue = 100;

    private ILightSectionBuilder lightSectionBuilder;
    private ICompositeLightBuilder compositeLightBuilder;
    private Light westLight;
    private Light eastLight;

    /// <summary>
    ///   The diagonal lights are composed from a percentage of the left light
    ///   and a percentage of the right.  This value determines the percentage of the side light
    ///   to use for a diagonal on that side.  For example, the NW light takes a percentage of the
    ///   West light equal to sidePercentageOnDiagonal.
    ///   The valid range taken by this value is detailed in the IsValid method.
    /// </summary>
    private int sidePercentageOnDiagonal;

    public CompositeLightSectionBuilder(ILightSectionBuilder lightSectionBuilder, ICompositeLightBuilder compositeLightBuilder)
    {
      this.lightSectionBuilder = lightSectionBuilder;
      this.compositeLightBuilder = compositeLightBuilder;
    }

    public CompositeLightSectionBuilder WithLights(Light westLight, Light eastLight)
    {
      this.westLight = westLight;
      this.eastLight = eastLight;
      return this;
    }

    public CompositeLightSectionBuilder WithSidePercentageOnDiagonal(int percentage)
    {
      sidePercentageOnDiagonal = percentage;
      return this;
    }

    public LightSection Build()
    {
      if (!IsValid())
      {
        var message = string.Format("Unable to build the LightSection.  This is either because something is missing (both the percentage and the lights must be specified) or because the given percentage value is invalid (it must be beteen {0} and {1})",
          sidePercentageMinValue,
          sidePercentageMaxValue);
        throw new ArgumentException(message);
      }

      var westDiagonalLight = compositeLightBuilder.BuildCompositeLight(westLight, eastLight, sidePercentageOnDiagonal);
      var centralLight = compositeLightBuilder.BuildCompositeLight(westLight, eastLight, centrePercentage);
      var eastDiagonalLight = compositeLightBuilder.BuildCompositeLight(westLight, eastLight, sidePercentageMaxValue - sidePercentageOnDiagonal);

      return lightSectionBuilder
        .WithLightInDirection(eDirection.North, centralLight)
        .WithLightInDirection(eDirection.NorthEast, eastDiagonalLight)
        .WithLightInDirection(eDirection.East, eastLight)
        .WithLightInDirection(eDirection.SouthEast, eastDiagonalLight)
        .WithLightInDirection(eDirection.South, centralLight)
        .WithLightInDirection(eDirection.SouthWest, westDiagonalLight)
        .WithLightInDirection(eDirection.West, westLight)
        .WithLightInDirection(eDirection.NorthWest, westDiagonalLight)
        .Build();
    }

    /// <remarks>
    ///   This value of sidePercentageOnDiagonal is expected to be between 50 and 100
    ///   (since the centre lights are hardcoded as 50% of each side light).
    /// </remarks>
    private bool IsValid()
    {
      return sidePercentageMinValue <= sidePercentageOnDiagonal &&
                   sidePercentageOnDiagonal <= sidePercentageMaxValue &&
             westLight != null &&
             eastLight != null;
    }
  }
}