using aPC.Client.Morse.Communication;
using aPC.Common.Client.Tests.Communication;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using aPC.Common.Client.Communication;

namespace aPC.Client.Morse.Tests.Communication
{
  [TestFixture]
  internal class NotificationClientTests
  {
    private NotificationClient client;
    private TestNotificationService host;
    private amBXScene arbitraryScene;

    [TestFixtureSetUp]
    public void SetupTests()
    {
      host = new TestNotificationService();
      client = new NotificationClient(host.Hostname);
      arbitraryScene = new DefaultScenes().Rainbow;
    }

    [TestFixtureTearDown]
    public void FixtureTearDown()
    {
      host.Dispose();
    }

    [TearDown]
    public void TearDown()
    {
      host.Scenes.Clear();
    }

    [Test]
    public void PushingAnIntegratedScene_ThrowsException()
    {
      Assert.Throws<CommunicationException>(() => client.PushSceneName("blah"));
    }

    [Test]
    public void PushingAScene_SuccesfullySendsScene()
    {
      client.PushScene(arbitraryScene);

      Assert.AreEqual(1, host.Scenes.Count);
      Assert.AreEqual(false, host.Scenes[0].Item1);

      var storedScene = (amBXScene) host.Scenes[0].Item2;
      //TODO: Test equality more comprehensively
    }
  }
}