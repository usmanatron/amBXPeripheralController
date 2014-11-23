using aPC.Common.Entities;
using aPC.Server.Communication;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace aPC.Server.Tests.Communication
{
  [TestFixture]
  internal class NotificationServiceTests
  {
    [TestFixtureSetUp]
    public void TestFixtureSetup()
    {
      mPushedScenes = new List<amBXScene>();
      mNotificationService = new NotificationService(scene => mPushedScenes.Add(scene));
    }

    [TearDown]
    public void TearDown()
    {
      mPushedScenes.Clear();
    }

    [Test]
    public void PushedCustomScene_Updated()
    {
      mNotificationService.RunCustomScene(File.ReadAllText("ExampleScene.xml"));

      Assert.AreEqual(1, mPushedScenes.Count);
      //qq
    }

    [Test]
    public void PushedValidIntegratedScene_Updated()
    {
      mNotificationService.RunIntegratedScene("Default_RedVsBlue");

      Assert.AreEqual(1, mPushedScenes.Count);
      //qq
    }

    [Test]
    public void PushedInvalidIntegratedScene_DefaultstoErrorFlash()
    {
      mNotificationService.RunIntegratedScene("TotallyInvalidSceneName");

      Assert.AreEqual(1, mPushedScenes.Count);
      //qq
    }

    private NotificationService mNotificationService;
    private List<amBXScene> mPushedScenes;
  }
}