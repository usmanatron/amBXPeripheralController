using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using aPC.Client.Disco.Generators;
using aPC.Client.Disco.Tests;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Client.Disco.Tests.Generators
{
  [TestFixture]
  class RandomLightSectionGeneratorTests
  {
    [TestFixtureSetUp]
    public void SetupGenerator()
    {
      var lSettings = new Settings();
      mGenerator = new RandomLightSectionGenerator(lSettings, new TestRandom(lSettings.ChangeThreshold / 2));
    }

    [Test]
    public void Generating_BuildsALightSection()
    {
      LightSection lSection = mGenerator.Generate();

      Assert.AreNotEqual(default(int), lSection.FadeTime);
    }

    [Test]
    public void Generating_GivesOnlyLightsInPhysicalDirections()
    {
      var lSection = mGenerator.Generate();

      Assert.IsNull(lSection.SouthEast);
      Assert.IsNull(lSection.South);
      Assert.IsNull(lSection.SouthWest);
    }

    private RandomLightSectionGenerator mGenerator;
  }
}
