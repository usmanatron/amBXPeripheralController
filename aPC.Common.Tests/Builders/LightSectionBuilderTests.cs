using NUnit.Framework;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using System;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  class LightSectionBuilderTests
  {
    [Test]
    public void NewLightSection_WithNoFadeTime_ThrowsException()
    {
      var lSectionBuilder = new LightSectionBuilder()
      .WithAllLights(mGreen);

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
      .WithLightInDirection(eDirection.East, mGreen);

      Assert.DoesNotThrow(() => lSectionBuilder.Build());
    }

    [Test]
    public void NewLightSection_HasValidFadeTime()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.East, mGreen)
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
        .WithLightInDirection(xiDirection, mGreen)
        .Build();

      Assert.AreEqual(mGreen, lSection.GetComponentValueInDirection(xiDirection));
    }

    [Test]
    public void NewLightSection_AllLightsSpecifiedInOneGo_AllLightsSpecified()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithAllLights(mGreen)
        .Build();

      Assert.AreEqual(mGreen, lSection.North);
      Assert.AreEqual(mGreen, lSection.NorthEast);
      Assert.AreEqual(mGreen, lSection.East);
      Assert.AreEqual(mGreen, lSection.SouthEast);
      Assert.AreEqual(mGreen, lSection.South);
      Assert.AreEqual(mGreen, lSection.SouthWest);
      Assert.AreEqual(mGreen, lSection.West);
      Assert.AreEqual(mGreen, lSection.NorthWest);
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
        .WithLightInDirection(eDirection.South, mGreen)
        .WithLightInDirectionIfPhysical(eDirection.South, mBlue)
        .Build();

      Assert.AreEqual(mGreen, lSection.South);
    }

    [Test]
    public void SpecifyingAPhysicalLightInAPhysicalDirection_UpdatesLight()
    {
      var lSectionBuilder = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirectionIfPhysical(eDirection.North, mGreen);

      Assert.DoesNotThrow(() => lSectionBuilder.Build());

      var lSection = lSectionBuilder.Build();

      Assert.AreEqual(mGreen, lSection.North);
    }

    private Light mOrange = DefaultLights.Orange;
    private Light mGreen = DefaultLights.Green;
    private Light mBlue = DefaultLights.Blue;
    private Light mRed = DefaultLights.Red;
  }
}
