using aPC.Client.Shared;
using aPC.Common.Client.Tests.Communication;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Client.Tests
{
  [TestFixture]
  internal class SceneRunnerTests
  {
    private TestNotificationClient testNotificationClient;
    private Settings settings;

    [SetUp]
    public void Setup()
    {
      testNotificationClient = new TestNotificationClient();
      settings = new Settings();
    }

    [Test]
    public void IntegratedScene_PushedAppropriately()
    {
      settings.SetSceneName("Scene_Name");
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
      var scene = new amBXScene();
      settings.SetScene(scene);
      var task = new SceneRunner(settings, testNotificationClient);

      task.RunScene();

      Assert.IsTrue(settings.IsValid);
      Assert.AreEqual(0, testNotificationClient.NumberOfIntegratedScenesPushed);
      Assert.AreEqual(1, testNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual(scene, testNotificationClient.CustomScenesPushed[0]);
    }
  }
}