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
    [Test]
    public void NewLightSection_WithNoFadeTime_ThrowsException()
    {
      var lSectionBuilder = new LightSectionBuilder()
        .WithAllLights(mAribitraryColour);

      Assert.Throws<ArgumentException>(() => lSectionBuilder.Build());
    }

    [Test]
    public void NewLightSection_WithNoLights_ThrowsException()
    {
      var lSectionBuilder = new LightSectionBuilder()
        .WithFadeTime(100);

      Assert.Throws<ArgumentException>(() => lSectionBuilder.Build());
    }

    [Test]
    public void NewLightSection_WithFadeTimeAndAtLeastOneLightSpecified_DoesNotThrowException()
    {
      var lSectionBuilder = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.East, mAribitraryColour);

      Assert.DoesNotThrow(() => lSectionBuilder.Build());
    }

    [Test]
    public void NewLightSection_HasValidFadeTime()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.East, mAribitraryColour)
        .Build();

      Assert.AreEqual(100, lSection.FadeTime);
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
    public void NewLightSection_HasValidLightInRightDirection(eDirection xiDirection)
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(xiDirection, mAribitraryColour)
        .Build();

      Assert.AreEqual(mAribitraryColour, lSection.GetComponentValueInDirection(xiDirection));
    }

    [Test]
    public void NewLightSection_AllLightsSpecifiedInOneGo_AllLightsSpecified()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithAllLights(mAribitraryColour)
        .Build();

      Assert.AreEqual(mAribitraryColour, lSection.North);
      Assert.AreEqual(mAribitraryColour, lSection.NorthEast);
      Assert.AreEqual(mAribitraryColour, lSection.East);
      Assert.AreEqual(mAribitraryColour, lSection.SouthEast);
      Assert.AreEqual(mAribitraryColour, lSection.South);
      Assert.AreEqual(mAribitraryColour, lSection.SouthWest);
      Assert.AreEqual(mAribitraryColour, lSection.West);
      Assert.AreEqual(mAribitraryColour, lSection.NorthWest);
    }

    [Test]
    public void NewLightSection_DifferentLightsInDifferentPlaces_OnTheRightPlaces()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.North, mGreen)
        .WithLightInDirection(eDirection.East, mBlue)
        .WithLightInDirection(eDirection.SouthWest, mRed)
        .WithLightInDirection(eDirection.NorthWest, mOrange)
        .Build();

      Assert.AreEqual(mGreen, lSection.North);
      Assert.AreEqual(null, lSection.NorthEast);
      Assert.AreEqual(mBlue, lSection.East);
      Assert.AreEqual(null, lSection.SouthEast);
      Assert.AreEqual(null, lSection.South);
      Assert.AreEqual(mRed, lSection.SouthWest);
      Assert.AreEqual(null, lSection.West);
      Assert.AreEqual(mOrange, lSection.NorthWest);
    }

    [Test]
    public void NewLightSection_SpecifyingALightInAnInvalidDirection_ThrowsException()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100);

      Assert.Throws<InvalidOperationException>(() => lSection.WithLightInDirection(eDirection.Center, mBlue));
    }

    /// <remarks>
    /// In order to confirm the light is not added, we show that the Builder is invalid
    /// It clearly has a fade time, so this must be from the lack of light.
    /// </remarks>
    [Test]
    public void SpecifyingAPhysicalLightInANonPhysicalLocation_DoesNotUpdateLight()
    {
      var lSectionBuilder = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirectionIfPhysical(eDirection.South, mBlue);

      Assert.Throws<ArgumentException>(() => lSectionBuilder.Build());
    }

    [Test]
    public void SpecifyingAPhysicalLightInaNonPhysicalDirection_DoesNotOverwritePreviousData()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.South, mAribitraryColour)
        .WithLightInDirectionIfPhysical(eDirection.South, mBlue)
        .Build();

      Assert.AreEqual(mAribitraryColour, lSection.South);
    }

    [Test]
    public void SpecifyingAPhysicalLightInAPhysicalDirection_UpdatesLight()
    {
      var lSectionBuilder = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirectionIfPhysical(eDirection.North, mAribitraryColour);

      var lSection = lSectionBuilder.Build();

      Assert.AreEqual(mAribitraryColour, lSection.North);
    }

    private readonly Light mAribitraryColour = DefaultLights.Green;
    private readonly Light mOrange = DefaultLights.Orange;
    private readonly Light mGreen = DefaultLights.Green;
    private readonly Light mBlue = DefaultLights.Blue;
    private readonly Light mRed = DefaultLights.Red;
  }
}