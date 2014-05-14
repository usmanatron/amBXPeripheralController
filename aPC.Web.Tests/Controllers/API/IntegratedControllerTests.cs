using aPC.Common.Client.Tests.Communication;
using aPC.Common;
using aPC.Web.Controllers.API;
using aPC.Web.Models;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Web.Tests.Controllers.API
{
  [TestFixture]
  public class IntegratedControllerTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
    }

    [SetUp]
    public void Setup()
    {
      mClient = new TestNotificationClient();
      mController = new IntegratedController(mClient);
    }

    [Test]
    public void TestGet_ReturnsAllIntegratedScenes()
    {
      var lIntegratedScenes = new SceneAccessor().GetAllScenes();

      IEnumerable<amBXSceneSummary> lResults = mController.Get();

      Assert.AreEqual(lIntegratedScenes.Count, lResults.Count());
      CollectionAssert.AreEquivalent(lIntegratedScenes.Select(scene => scene.Key), lResults.Select(scene => scene.SceneName));
    }


    [Test]
    public void GetById_ReturnsExpectedScene()
    {
      var lScene = new SceneAccessor().GetScene("poolq2_event");
      var lResult = mController.Get("poolq2_event");

      Assert.AreEqual(lScene.SceneType, lResult.SceneType);
      Assert.AreEqual(lScene.FrameStatistics.SceneLength, lResult.FrameStatistics.SceneLength);
      Assert.AreEqual(lScene.FrameStatistics.EnabledDirectionalComponents, lResult.FrameStatistics.EnabledDirectionalComponents);
    }

    [Test]
    public void Post_SendsExpectedScene()
    {
      mController.Post("poolq2_event");

       Assert.AreEqual(1, mClient.NumberOfIntegratedScenesPushed);
       Assert.AreEqual(0, mClient.NumberOfCustomScenesPushed);
       Assert.AreEqual("poolq2_event", mClient.IntegratedScenesPushed.First());
    }

    private IntegratedController mController;
    private TestNotificationClient mClient;
  }
}
