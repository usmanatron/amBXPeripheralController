using System.Collections.Generic;
using System.Linq;
using aPC.Common.Client.Tests.Communication;
using aPC.Common.Entities;
using aPC.Common;
using aPC.Web.Controllers.API;
using aPC.Web.Helpers;
using aPC.Web.Models;
using NUnit.Framework;

namespace aPC.Web.Tests.Controllers.API
{
  [TestFixture]
  public class IntegratedControllerTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      mHost = new TestNotificationService();
    }

    [Test]
    public void TestGet_ReturnsAllIntegratedScenes()
    {
      var lIntegratedScenes = new SceneAccessor().GetAllScenes();

      IEnumerable<amBXSceneSummary> lResults = new IntegratedController().Get();

      Assert.AreEqual(lIntegratedScenes.Count, lResults.Count());
      CollectionAssert.AreEquivalent(lIntegratedScenes.Select(scene => scene.Key), lResults.Select(scene => scene.SceneName));
    }


    [Test]
    public void GetById()
    {
      var lScene = new SceneAccessor().GetScene("poolq2_event");
      var lResult = new IntegratedController().Get("poolq2_event");

      Assert.AreEqual(lScene.SceneType, lResult.SceneType);
      Assert.AreEqual(lScene.FrameStatistics.SceneLength, lResult.FrameStatistics.SceneLength);
      Assert.AreEqual(lScene.FrameStatistics.EnabledDirectionalComponents, lResult.FrameStatistics.EnabledDirectionalComponents);
    }

    [Test]
    public void Post()
    {
      var lClient = new NotificationClient(mHost.Url);



      // Arrange
      IntegratedController controller = new IntegratedController();

      // Act
      controller.Post("value");

      // Assert
    }

    private TestNotificationService mHost;
  }
}
