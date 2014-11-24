using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  internal class LightSectionBuilderTests
  {
    private readonly Light aribitraryColour = DefaultLights.Green;
    private readonly Light orange = DefaultLights.Orange;
    private readonly Light green = DefaultLights.Green;
    private readonly Light blue = DefaultLights.Blue;
    private readonly Light red = DefaultLights.Red;

    [Test]
    public void NewLightSection_WithNoLights_ThrowsException()
    {
      var sectionBuilder = new LightSectionBuilder();

      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void NewLightSection_WithFadeTimeAndAtLeastOneLightSpecified_DoesNotThrowException()
    {
      var sectionBuilder = new LightSectionBuilder()
        .WithLightInDirection(eDirection.East, aribitraryColour);

      Assert.DoesNotThrow(() => sectionBuilder.Build());
    }

    [Test]
    [TestCase(eDirection.North)]
    [TestCase(eDirection.NorthEast)]
    [TestCase(eDirection.East)]
    [TestCase(eDirection.SouthEast)]
    [TestCase(eDirection.South)]
    [TestCase(eDirection.SouthWest)]
    [TestCase(eDirection.West)]
    [TestCase(eDirection.NorthWest)]
    public void NewLightSection_HasValidLightInRightDirection(eDirection direction)
    {
      var section = new LightSectionBuilder()
        .WithLightInDirection(direction, aribitraryColour)
        .Build();

      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(direction));
    }

    [Test]
    public void NewLightSection_AllLightsSpecifiedInOneGo_AllLightsSpecified()
    {
      var section = new LightSectionBuilder()
        .WithAllLights(aribitraryColour)
        .Build();

      Assert.AreEqual(aribitraryColour, section.North);
      Assert.AreEqual(aribitraryColour, section.NorthEast);
      Assert.AreEqual(aribitraryColour, section.East);
      Assert.AreEqual(aribitraryColour, section.SouthEast);
      Assert.AreEqual(aribitraryColour, section.South);
      Assert.AreEqual(aribitraryColour, section.SouthWest);
      Assert.AreEqual(aribitraryColour, section.West);
      Assert.AreEqual(aribitraryColour, section.NorthWest);
    }

    [Test]
    public void NewLightSection_DifferentLightsInDifferentPlaces_OnTheRightPlaces()
    {
      var section = new LightSectionBuilder()
        .WithLightInDirection(eDirection.North, green)
        .WithLightInDirection(eDirection.East, blue)
        .WithLightInDirection(eDirection.SouthWest, red)
        .WithLightInDirection(eDirection.NorthWest, orange)
        .Build();

      Assert.AreEqual(green, section.North);
      Assert.AreEqual(null, section.NorthEast);
      Assert.AreEqual(blue, section.East);
      Assert.AreEqual(null, section.SouthEast);
      Assert.AreEqual(null, section.South);
      Assert.AreEqual(red, section.SouthWest);
      Assert.AreEqual(null, section.West);
      Assert.AreEqual(orange, section.NorthWest);
    }

    [Test]
    public void NewLightSection_SpecifyingALightInAnInvalidDirection_ThrowsException()
    {
      var section = new LightSectionBuilder();

      Assert.Throws<InvalidOperationException>(() => section.WithLightInDirection(eDirection.Center, blue));
    }

    /// <remarks>
    /// In order to confirm the light is not added, we show that the Builder is invalid
    /// It clearly has a fade time, so this must be from the lack of light.
    /// </remarks>
    [Test]
    public void SpecifyingAPhysicalLightInANonPhysicalLocation_DoesNotUpdateLight()
    {
      var sectionBuilder = new LightSectionBuilder()
        .WithLightInDirectionIfPhysical(eDirection.South, blue);

      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void SpecifyingAPhysicalLightInaNonPhysicalDirection_DoesNotOverwritePreviousData()
    {
      var section = new LightSectionBuilder()
        .WithLightInDirection(eDirection.South, aribitraryColour)
        .WithLightInDirectionIfPhysical(eDirection.South, blue)
        .Build();

      Assert.AreEqual(aribitraryColour, section.South);
    }

    [Test]
    public void SpecifyingAPhysicalLightInAPhysicalDirection_UpdatesLight()
    {
      var sectionBuilder = new LightSectionBuilder()
        .WithLightInDirectionIfPhysical(eDirection.North, aribitraryColour);

      var section = sectionBuilder.Build();

      Assert.AreEqual(aribitraryColour, section.North);
    }
  }
}