using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Entities;
using System;

namespace aPC.Chromesthesia.Lights
{
  internal class CompositeLightSectionBuilder
  {
    private LightSectionBuilder lightSectionBuilder;
    private CompositeLightBuilder compositeLightBuilder;
    private Light westLight;
    private Light eastLight;
    private int sidePercentageOnDiagonal;
    private readonly int fadeTime;

    private const int centrePercentage = 50;

    public CompositeLightSectionBuilder(LightSectionBuilder lightSectionBuilder, CompositeLightBuilder compositeLightBuilder)
    {
      this.lightSectionBuilder = lightSectionBuilder;
      this.sidePercentageOnDiagonal = -1;
      this.compositeLightBuilder = compositeLightBuilder;
      this.fadeTime = ChromesthesiaConfig.LightFadeTime;
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
    ///   to use for a diagonal on that side.  For example, the NW light takes a percentage of the
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
      var eastDiagonalLight = compositeLightBuilder.BuildCompositeLight(eastLight, westLight, sidePercentageOnDiagonal);

      return lightSectionBuilder
        .WithLightInDirection(eDirection.East, eastLight)
        .WithLightInDirection(eDirection.West, westLight)
        .WithLightInDirections(new[] { eDirection.North, eDirection.South }, centralLight)
        .WithLightInDirections(new[] { eDirection.NorthEast, eDirection.SouthEast }, eastDiagonalLight)
        .WithLightInDirections(new[] { eDirection.NorthWest, eDirection.SouthWest }, westDiagonalLight)
        .Build();
    }
  }
}