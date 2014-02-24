using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Common.Tests
{
  [TestFixture]
  class SceneAccessorTests
  {
    [TestFixtureSetUp]
    public void Setup()
    {
      mSeceneAccessor = new SceneAccessor();
    }

    [Test]
    [TestCase("error_flash")]
    [TestCase("Error_Flash")]
    [TestCase("eRrOr_FLaSh")]
    public void ValidSceneName_ReturnsScene_RegardlessOfCase(string xiSceneName)
    {
      var lScene = mSeceneAccessor.GetScene(xiSceneName);
      Assert.IsNotNull(lScene);
      Assert.IsTrue(lScene is amBXScene);
    }

    [Test]
    public void InvalidSceneName_ReturnsNull()
    {
      Assert.IsNull(mSeceneAccessor.GetScene("TotallyInvalidScene"));
    }

    private SceneAccessor mSeceneAccessor;
  }
}
