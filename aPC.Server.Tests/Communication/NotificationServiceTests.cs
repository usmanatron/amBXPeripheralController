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
    private NotificationService notificationService;
    private List<amBXScene> pushedScenes;

    [TestFixtureSetUp]
    public void TestFixtureSetup()
    {
      pushedScenes = new List<amBXScene>();
      notificationService = new NotificationService(scene => pushedScenes.Add(scene));
    }

    [TearDown]
    public void TearDown()
    {
      pushedScenes.Clear();
    }

    // TODO: Potentially add more assertions to the 3 tests below:

    [Test]
    public void PushedCustomScene_Updated()
    {
      notificationService.RunCustomScene(File.ReadAllText("ExampleScene.xml"));

      Assert.AreEqual(1, pushedScenes.Count);
    }

    [Test]
    public void PushedValidIntegratedScene_Updated()
    {
      notificationService.RunIntegratedScene("Default_RedVsBlue");

      Assert.AreEqual(1, pushedScenes.Count);
    }

    [Test]
    public void PushedInvalidIntegratedScene_DefaultstoErrorFlash()
    {
      notificationService.RunIntegratedScene("TotallyInvalidSceneName");

      Assert.AreEqual(1, pushedScenes.Count);
    }
  }
}