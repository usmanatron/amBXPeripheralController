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

      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.North));
      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.NorthEast));
      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.East));
      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.SouthEast));
      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.South));
      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.SouthWest));
      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.West));
      Assert.AreEqual(aribitraryColour, section.GetComponentValueInDirection(eDirection.NorthWest));
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

      Assert.AreEqual(green, section.GetComponentValueInDirection(eDirection.North));
      Assert.AreEqual(null, section.GetComponentValueInDirection(eDirection.NorthEast));
      Assert.AreEqual(blue, section.GetComponentValueInDirection(eDirection.East));
      Assert.AreEqual(null, section.GetComponentValueInDirection(eDirection.SouthEast));
      Assert.AreEqual(null, section.GetComponentValueInDirection(eDirection.South));
      Assert.AreEqual(red, section.GetComponentValueInDirection(eDirection.SouthWest));
      Assert.AreEqual(null, section.GetComponentValueInDirection(eDirection.West));
      Assert.AreEqual(orange, section.GetComponentValueInDirection(eDirection.NorthWest));
    }

    [Test]
    public void NewLightSection_SpecifyingALightInAnInvalidDirection_ThrowsException()
    {
      var section = new LightSectionBuilder();

      Assert.Throws<InvalidOperationException>(() => section.WithLightInDirection(eDirection.Center, blue));
    }
  }
}