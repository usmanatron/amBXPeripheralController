using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;

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

      Assert.AreEqual(aribitraryColour, section.GetComponentSectionInDirection(direction));
    }

    [Test]
    public void NewLightSection_AllLightsSpecifiedInOneGo_AllLightsSpecified()
    {
      var section = new LightSectionBuilder()
        .WithAllLights(aribitraryColour)
        .Build();

      foreach (var direction in EnumExtensions.GetCompassDirections())
      {
        Assert.AreEqual(aribitraryColour, section.GetComponentSectionInDirection(direction));
      }
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

      Assert.AreEqual(green, section.GetComponentSectionInDirection(eDirection.North));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.NorthEast));
      Assert.AreEqual(blue, section.GetComponentSectionInDirection(eDirection.East));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.SouthEast));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.South));
      Assert.AreEqual(red, section.GetComponentSectionInDirection(eDirection.SouthWest));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.West));
      Assert.AreEqual(orange, section.GetComponentSectionInDirection(eDirection.NorthWest));
    }

    [Test]
    public void NewLightSection_LightsSpecifiedInABlockOfDirections_SpecifiedInRightDirectons()
    {
      var section = new LightSectionBuilder()
        .WithLightInDirections(new List<eDirection> { eDirection.North, eDirection.East, eDirection.South, eDirection.West }, green)
        .Build();

      Assert.AreEqual(green, section.GetComponentSectionInDirection(eDirection.North));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.NorthEast));
      Assert.AreEqual(green, section.GetComponentSectionInDirection(eDirection.East));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.SouthEast));
      Assert.AreEqual(green, section.GetComponentSectionInDirection(eDirection.South));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.SouthWest));
      Assert.AreEqual(green, section.GetComponentSectionInDirection(eDirection.West));
      Assert.AreEqual(null, section.GetComponentSectionInDirection(eDirection.NorthWest));
    }
  }
}