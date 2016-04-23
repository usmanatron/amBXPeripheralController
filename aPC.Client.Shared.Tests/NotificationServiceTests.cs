using aPC.Client.Shared;
using aPC.Common.Client.Tests.Communication;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Client.Tests.Communication
{
  [TestFixture]
  internal class NotificationServiceTests
  {
    private TestNotificationService host;
    private NotificationClient client;

    [TestFixtureSetUp]
    public void SetupTests()
    {
      host = new TestNotificationService();
      client = new NotificationClient(host.Hostname);
    }

    [SetUp]
    public void ClearHost()
    {
      host.ClearScenes();
    }

    [TestFixtureTearDown]
    public void TearDownTests()
    {
      host.Dispose();
    }

    [Test]
    public void PushingACustomScene_SendsTheExpectedScene()
    {
      var scene = new amBXScene();
      client.PushScene(scene);

      Assert.AreEqual(1, host.Scenes.Count);
      Assert.AreEqual(false, host.Scenes[0].Item1);
      //TODO: More comprehensive equality tests
    }

    [Test]
    public void PushingAnIntegratedScene_SendsTheExpectedScene()
    {
      client.PushSceneName("IntegratedScene");

      Assert.AreEqual(1, host.Scenes.Count);
      Assert.AreEqual(true, host.Scenes[0].Item1);
      Assert.AreEqual("IntegratedScene", host.Scenes[0].Item2);
    }
  }
}