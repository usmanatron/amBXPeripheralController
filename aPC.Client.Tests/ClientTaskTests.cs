using aPC.Client;
using aPC.Common.Communication.Tests;
using NUnit.Framework;

namespace aPC.Client.Tests
{
  [TestFixture]
  class ClientTaskTests
  {
    [SetUp]
    public void Setup()
    {
      mTestNotificationClient = new TestNotificationClient();
    }

    [Test]
    public void IntegratedScene_PushedAppropriately()
    {
      var lSettings = new Settings(true, "Scene_Name");
      var lTask = new ClientTask(lSettings, mTestNotificationClient);

      lTask.Push();

      Assert.AreEqual(1, mTestNotificationClient.NumberOfIntegratedScenesPushed);
      Assert.AreEqual(0, mTestNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual("Scene_Name", mTestNotificationClient.IntegratedScenesPushed[0]);
    }

    [Test]
    public void CustomScene_PushedAppropriately()
    {
      var lSettings = new Settings(false, "CustomScene");
      var lTask = new ClientTask(lSettings, mTestNotificationClient);

      lTask.Push();

      Assert.AreEqual(0, mTestNotificationClient.NumberOfIntegratedScenesPushed);
      Assert.AreEqual(1, mTestNotificationClient.NumberOfCustomScenesPushed);
      Assert.AreEqual("CustomScene", mTestNotificationClient.CustomScenesPushed[0]);
    }

    private TestNotificationClient mTestNotificationClient;
  }
}
