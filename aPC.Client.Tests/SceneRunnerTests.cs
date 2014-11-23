using aPC.Common.Client.Tests.Communication;
using NUnit.Framework;

namespace aPC.Client.Tests
{
  [TestFixture]
  internal class SceneRunnerTests
  {
    private TestNotificationClient testNotificationClient;

    [SetUp]
    public void Setup()
    {
      testNotificationClient = new TestNotificationClient();
    }

    //TODO: Move this to a better place
    [Test]
    public void MissingSceneData_GivesInvalidSettings()
    {
      var settings = new Settings(true, string.Empty);

      Assert.IsFalse(settings.IsValid);
    }

    [Test]
    public void IntegratedScene_PushedAppropriately()
    {
      var settings = new Settings(true, "Scene_Name");
      var task = new SceneRunner(settings, testNotificationClient);

      task.RunScene();

      Assert.IsTrue(settings.IsValid);
      Assert.AreEqual(1, testNotificationClient.NumberOfIntegratedScenesPushed);
      Assert.AreEqual(0, testNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual("Scene_Name", testNotificationClient.IntegratedScenesPushed[0]);
    }

    [Test]
    public void CustomScene_PushedAppropriately()
    {
      var settings = new Settings(false, "CustomScene");
      var task = new SceneRunner(settings, testNotificationClient);

      task.RunScene();

      Assert.IsTrue(settings.IsValid);
      Assert.AreEqual(0, testNotificationClient.NumberOfIntegratedScenesPushed);
      Assert.AreEqual(1, testNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual("CustomScene", testNotificationClient.CustomScenesPushed[0]);
    }
  }
}