using aPC.Client.Disco.Generators;
using aPC.Common.Client;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Client.Disco.Tests.Generators
{
  [TestFixture]
  internal class RandomLightSectionGeneratorTests
  {
    private RandomLightSectionGenerator generator;

    [TestFixtureSetUp]
    public void SetupGenerator()
    {
      var settings = new Settings(new HostnameAccessor());
      generator = new RandomLightSectionGenerator(settings, new TestRandom(settings.ChangeThreshold / 2));
    }

    [Test]
    public void Generating_BuildsALightSection()
    {
      LightSection section = generator.Generate();

      Assert.AreNotEqual(default(int), section.FadeTime);
    }

    [Test]
    public void Generating_GivesOnlyLightsInPhysicalDirections()
    {
      var section = generator.Generate();

      Assert.IsNull(section.SouthEast);
      Assert.IsNull(section.South);
      Assert.IsNull(section.SouthWest);
    }
  }
}