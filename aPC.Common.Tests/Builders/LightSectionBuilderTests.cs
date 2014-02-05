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

      Assert.AreEqual(mGreen, GetLightForDirection(lSection, xiDirection));
    }

    //qqUMI TODO: Consider if this is the best way to do this
    private Light GetLightForDirection(LightSection xiSection, eDirection xiDirection)
    {
      var lSectionBuilderBase = new SectionBuilderBase();
      var lFieldInfo = lSectionBuilderBase.GetComponentInfoInDirection(xiSection, xiDirection);
      return lFieldInfo.GetValue(xiSection) as Light;
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

    private Light mOrange = DefaultLights.Orange;
    private Light mGreen = DefaultLights.Green;
    private Light mBlue = DefaultLights.Blue;
    private Light mRed = DefaultLights.Red;
  }
}
