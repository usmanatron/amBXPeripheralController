using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Builders;
using System;

namespace aPC.Chromesthesia.Server
{
  class CompositeLightSectionBuilder
  {
    private LightSectionBuilder lightSectionBuilder;
    private CompositeLightBuilder compositeLightBuilder;
    private Light westLight;
    private Light eastLight;
    private int sidePercentageOnDiagonal;

    // Settings
    private const int centrePercentage = 50;
    private const int fadeTime = 2;

    public CompositeLightSectionBuilder(LightSectionBuilder lightSectionBuilder, CompositeLightBuilder compositeLightBuilder)
    {
      this.lightSectionBuilder = lightSectionBuilder;
      this.sidePercentageOnDiagonal = -1;
      this.compositeLightBuilder = compositeLightBuilder;
    }

    public CompositeLightSectionBuilder WithLights(Light westLight, Light eastLight)
    {
      this.westLight = westLight;
      this.eastLight = eastLight;
      return this;
    }

    /// <summary>
    ///   Set the value of sidePercentageOnDiagonal.
    ///   
    /// The diagonal lights are composed from a percentage of the left light 
    ///   and a percentage of the right.  This value determines the percentage of the side light
    ///   to use for a dagonal on that side.  For example, the NW light takes a percentage of the 
    ///   West light equal to sidePercentageOnDiagonal.
    /// </summary>
    /// <remarks>
    ///   This value is expected to be between 50 and 100  (since the centre lights are hardcoded 
    ///   as 50% of each side light).
    /// </remarks>
    public CompositeLightSectionBuilder WithSidePercentageOnDiagonal(int percentage)
    {
      if (!percentageInExpectedRange(percentage))
      {
        throw new ArgumentException("Unexpected value");
      }

      sidePercentageOnDiagonal = percentage;
      return this;
    }

    private bool percentageInExpectedRange(int value)
    {
      return 50 <= value && value <= 100;
    }

    public LightSection Build()
    {
      var westDiagonalLight = compositeLightBuilder.BuildCompositeLight(westLight, eastLight, sidePercentageOnDiagonal);
      var centralLight = compositeLightBuilder.BuildCompositeLight(westLight, eastLight, centrePercentage);
      var eastDiagonalLight = compositeLightBuilder.BuildCompositeLight(westLight, eastLight, sidePercentageOnDiagonal);

      return lightSectionBuilder
        .WithLightInDirection(eDirection.North, centralLight)
        .WithLightInDirection(eDirection.NorthEast, eastDiagonalLight)
        .WithLightInDirection(eDirection.East, eastLight)
        .WithLightInDirection(eDirection.SouthEast, eastDiagonalLight)
        .WithLightInDirection(eDirection.South, centralLight)
        .WithLightInDirection(eDirection.SouthWest, westDiagonalLight)
        .WithLightInDirection(eDirection.West, westLight)
        .WithLightInDirection(eDirection.NorthWest, westDiagonalLight)
        .WithFadeTime(fadeTime)
        .Build();
    }
  }
}