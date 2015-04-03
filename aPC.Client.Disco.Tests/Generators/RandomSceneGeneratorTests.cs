using aPC.Client.Disco.Generators;
using aPC.Common.Client;
using NUnit.Framework;

namespace aPC.Client.Disco.Tests.Generators
{
  [TestFixture]
  internal class RandomSceneGeneratorTests
  {
    private Settings settings;
    private RandomSceneGenerator generator;

    [TestFixtureSetUp]
    public void SetupGenerator()
    {
      settings = new Settings(new HostnameAccessor());
      generator = new RandomSceneGenerator(settings, new TestLightSectionGenerator());
    }

    [Test]
    public void NewScene_HasSpecificNumberOfFrames()
    {
      var scene = generator.Generate();
      Assert.AreEqual(settings.FramesPerScene, scene.Frames.Count);
    }

    [Test]
    public void NewScene_DoesNotSpecifyFansOrRumbles()
    {
      var scene = generator.Generate();

      Assert.IsNull(scene.Frames[0].FanSection);
      Assert.IsNull(scene.Frames[0].RumbleSection);
    }
  }
}