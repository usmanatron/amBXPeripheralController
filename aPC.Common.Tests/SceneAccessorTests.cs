using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Common.Tests
{
  [TestFixture]
  internal class SceneAccessorTests
  {
    private SceneAccessor sceneAccessor;

    [TestFixtureSetUp]
    public void Setup()
    {
      sceneAccessor = new SceneAccessor(new DefaultScenes());
    }

    [Test]
    [TestCase("error_flash")]
    [TestCase("Error_Flash")]
    [TestCase("eRrOr_FLaSh")]
    public void ValidSceneName_ReturnsScene_RegardlessOfCase(string sceneName)
    {
      var scene = sceneAccessor.GetScene(sceneName);
      Assert.IsNotNull(scene);
      Assert.IsTrue(scene is amBXScene);
    }

    [Test]
    public void InvalidSceneName_ReturnsNull()
    {
      Assert.IsNull(sceneAccessor.GetScene("TotallyInvalidScene"));
    }
  }
}