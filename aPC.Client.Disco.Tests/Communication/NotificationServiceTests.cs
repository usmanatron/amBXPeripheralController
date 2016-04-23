using aPC.Client.Disco.Communication;
using aPC.Common.Client.Tests.Communication;
using NUnit.Framework;
using System;

namespace aPC.Client.Disco.Tests.Communication
{
  [TestFixture]
  internal class NotificationServiceTests
  {
    private NotificationClient client;
    private TestNotificationService host;

    [TestFixtureSetUp]
    public void SetupTests()
    {
      host = new TestNotificationService();
      client = new NotificationClient(host.Hostname);
    }

    [TestFixtureTearDown]
    public void TearDownTests()
    {
      host.Dispose();
    }

    [Test]
    public void PushingAnIntegratedScene_ThrowsException()
    {
      Assert.Throws<NotSupportedException>(() => client.PushSceneName("ccnet_green"));
    }

    [Test]
    public void PushingACustomScene_SendsTheExpectedScene()
    {
      var scene = new aPC.Common.Defaults.DefaultScenes().Building;
      client.PushScene(scene);

      Assert.AreEqual(1, host.Scenes.Count);
      Assert.AreEqual(false, host.Scenes[0].Item1);
      Assert.AreEqual(scene, host.Scenes[0].Item2);
    }
  }
}