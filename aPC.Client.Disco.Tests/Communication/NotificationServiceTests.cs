using System;
using NUnit.Framework;
using aPC.Client.Disco.Communication;
using aPC.Common.Communication;
using aPC.Common.Communication.Tests;

namespace aPC.Client.Disco.Tests.Communication
{
  [TestFixture]
  class NotificationServiceTests
  {
    [TestFixtureSetUp]
    public void SetupTests()
    {
      var lUrl = CommunicationSettings.ServiceUrlTemplate
        .Replace(CommunicationSettings.HostnameHolder, "localhost")
        .Replace("amBXPeripheralController", "aPCTest");

      mHost = new TestNotificationService(lUrl);
      mClient = new NotificationClient(lUrl);
    }

    [TestFixtureTearDown]
    public void TearDownTests()
    {
      mHost.Dispose();
    }

    [Test]
    public void PushingAnIntegratedScene_ThrowsException()
    {
      Assert.Throws<NotImplementedException>(() => mClient.PushIntegratedScene("ccnet_green"));
    }

    [Test]
    public void PushingADodgyCustomScene_ExceptionsHandled()
    {
      Assert.DoesNotThrow(() => mClient.PushCustomScene("ThrowException"));
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
