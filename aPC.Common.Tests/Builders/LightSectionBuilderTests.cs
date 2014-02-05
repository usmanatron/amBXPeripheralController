using NUnit.Framework;
using aPC.Common.Builders;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  class LightSectionBuilderTests
  {

    //NEW Test.  If repeated or fade time is missing, throw an exception when building!

    [Test]
    public void EmptyLightSection_IsEmpty()
    {
      var lSection = new LightSectionBuilder()
        .Build();

      Assert.AreEqual(default(int), lSection.FadeTime);
      Assert.AreEqual(null, lSection.NorthEast);
    }

    [Test]
    public void FadeTimeSaved()
    {
      var lSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .Build();

      Assert.AreEqual(100, lSection.FadeTime);
    }

    [Test]
    //qqUMI Would ideally have different cases here for different lights
    // and directions except I can't do this (as Light isn't a constant at compile time.
    public void NewLightSection_OneLightSpecified_OnTheRightPlace()
    {
      var lSection = new LightSectionBuilder()
        .WithLightInDirection(eDirection.North, Defaults.DefaultLights.Green)
        .Build();

      Assert.AreEqual(Defaults.DefaultLights.Green, lSection.North);
    }

    [Test]
    public void NewLightSection_AllLightsSpecifiedInOneGo_OnTheRightPlace()
    {
      var lSection = new LightSectionBuilder()
        .WithAllLights(Defaults.DefaultLights.Blue)
        .Build();

      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.North);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.NorthEast);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.East);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.SouthEast);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.South);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.SouthWest);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.West);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.NorthWest);
    }

    [Test]
    public void NewLightSection_DifferentLightsInDifferentPlaces_OnTheRightPlaces()
    {
      var lSection = new LightSectionBuilder()
        .WithLightInDirection(eDirection.North, Defaults.DefaultLights.Green)
        .WithLightInDirection(eDirection.East, Defaults.DefaultLights.Blue)
        .WithLightInDirection(eDirection.SouthWest, Defaults.DefaultLights.Red)
        .WithLightInDirection(eDirection.NorthWest, Defaults.DefaultLights.Orange)
        .Build();

      Assert.AreEqual(Defaults.DefaultLights.Green, lSection.North);
      Assert.AreEqual(null, lSection.NorthEast);
      Assert.AreEqual(Defaults.DefaultLights.Blue, lSection.East);
      Assert.AreEqual(null, lSection.SouthEast);
      Assert.AreEqual(null, lSection.South);
      Assert.AreEqual(Defaults.DefaultLights.Red, lSection.SouthWest);
      Assert.AreEqual(null, lSection.West);
      Assert.AreEqual(Defaults.DefaultLights.Orange, lSection.NorthWest);
    }
  }
}
