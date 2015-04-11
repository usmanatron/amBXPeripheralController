using aPC.Common;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  internal class LightSectionBuilderTests
  {
    private LightSectionBuilder sectionBuilder;

    private readonly Light aribitraryColour = DefaultLights.Green;
    private readonly Light orange = DefaultLights.Orange;
    private readonly Light green = DefaultLights.Green;
    private readonly Light blue = DefaultLights.Blue;
    private readonly Light red = DefaultLights.Red;
    private eDirection[] compassDirections = EnumExtensions.GetCompassDirections().ToArray();

    [SetUp]
    public void Setup()
    {
      sectionBuilder = new LightSectionBuilder();
    }

    [Test]
    public void NewLightSection_WithNoLights_ThrowsException()
    {
      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void NewLightSection_WithAtLeastOneLightSpecified_DoesNotThrowException()
    {
      sectionBuilder.WithLightInDirection(eDirection.East, aribitraryColour);

      Assert.DoesNotThrow(() => sectionBuilder.Build());
    }

    [Test]
    [TestCaseSource("compassDirections")]
    public void NewLightSection_HasValidLightInRightDirection(eDirection direction)
    {
      var section = sectionBuilder
        .WithLightInDirection(direction, aribitraryColour)
        .Build();

      Assert.AreEqual(aribitraryColour, section.GetComponentSectionInDirection(direction));
    }

    [Test]
    [TestCaseSource("compassDirections")]
    public void SpecifyingALightOnTheSameDirectionTwice_Throws(eDirection direction)
    {
      sectionBuilder.WithLightInDirection(direction, aribitraryColour);

      Assert.Throws<ArgumentException>(() => sectionBuilder.WithLightInDirection(direction, aribitraryColour));
    }

    [Test]
    public void SpecifyingALightWithoutAFadeTime_Throws()
    {
      var colourWithoutFadeTime = (Light)aribitraryColour.Clone();
      colourWithoutFadeTime.FadeTime = 0;

      Assert.Throws<ArgumentException>(() => sectionBuilder.WithLightInDirection(eDirection.North, colourWithoutFadeTime));
    }

    [Test]
    public void NewLightSection_AllLightsSpecifiedInOneGo_AllLightsSpecified()
    {
      var section = sectionBuilder
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
      var section = sectionBuilder
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
    public void SpecifyingTheSameLightInMultipleDirections_AppliesTheLightAsExpected()
    {
      var section = sectionBuilder
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