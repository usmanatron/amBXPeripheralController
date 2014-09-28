using System;
using System.ServiceModel;
using NUnit.Framework;
using aPC.Client.Disco.Communication;
using aPC.Common.Client.Tests.Communication;

namespace aPC.Client.Disco.Tests.Communication
{
  [TestFixture]
  class NotificationServiceTests
  {
    [TestFixtureSetUp]
    public void SetupTests()
    {
      mHost = new TestNotificationService();
      mClient = new NotificationClient(new EndpointAddress(mHost.Url));
    }

    [TestFixtureTearDown]
    public void TearDownTests()
    {
      mHost.Dispose();
    }

    [Test]
    public void PushingAnIntegratedScene_ThrowsException()
    {
      Assert.Throws<NotSupportedException>(() => mClient.PushIntegratedScene("ccnet_green"));
    }

    [Test]
    public void PushingACustomScene_SendsTheExpectedScene()
    {
      mClient.PushCustomScene("scene");

      Assert.AreEqual(1, mHost.Scenes.Count);
      Assert.AreEqual(false, mHost.Scenes[0].Item1);
      Assert.AreEqual("scene", mHost.Scenes[0].Item2);
    }

    private NotificationClient mClient;
    private TestNotificationService mHost;
  }
}
