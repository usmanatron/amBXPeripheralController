using aPC.Client.Disco.Generators;
using aPC.Common.Defaults;
using NUnit.Framework;

namespace aPC.Client.Disco.Tests.Generators
{
  [TestFixture]
  class RandomSceneGeneratorTests
  {
    [TestFixtureSetUp]
    public void SetupGenerator()
    {
      mSettings = new Settings();
      mGenerator = new RandomSceneGenerator(mSettings, new TestLightSectionGenerator());
    }

    [Test]
    public void NewScene_HasSpecificNumberOfFrames()
    {
      var lScene = mGenerator.Generate();
      Assert.AreEqual(mSettings.FramesPerScene, lScene.Frames.Count);
    }

    [Test]
    public void NewScene_DoesNotSpecifyFansOrRumbles()
    {
      var lScene = mGenerator.Generate();

      Assert.IsNull(lScene.Frames[0].Fans);
      Assert.IsNull(lScene.Frames[0].Rumbles);
    }

    private Settings mSettings;
    private RandomSceneGenerator mGenerator;
  }
}
