using System.Net;
using System.Web.Http;
using aPC.Common.Client.Tests.Communication;
using aPC.Common;
using aPC.Web.Controllers.API;
using NUnit.Framework;
using System.Linq;
using aPC.Common.Defaults;

namespace aPC.Web.Tests.Controllers.API
{
  [TestFixture]
  public class IntegratedControllerTests
  {
    [SetUp]
    public void Setup()
    {
      mClient = new TestNotificationClient();
      mController = new IntegratedController(mClient);
    }

    [Test]
    public void GetWithoutName_ReturnsAllIntegratedScenes()
    {
      var lIntegratedScenes = new SceneAccessor(new DefaultScenes()).GetAllScenes();

      var lResults = mController.Get();

      Assert.AreEqual(lIntegratedScenes.Count, lResults.Count());
      CollectionAssert.AreEquivalent(lIntegratedScenes.Select(scene => scene.Key), lResults.Select(scene => scene.SceneName));
    }


    [Test]
    public void GetByName_ReturnsExpectedScene()
    {
      var lScene = new SceneAccessor(new DefaultScenes()).GetScene("poolq2_event");

      var lResult = mController.Get("poolq2_event");

      Assert.AreEqual(lScene.SceneType, lResult.SceneType);
      Assert.AreEqual(lScene.FrameStatistics.SceneLength, lResult.FrameStatistics.SceneLength);
      Assert.AreEqual(lScene.FrameStatistics.EnabledDirectionalComponents, lResult.FrameStatistics.EnabledDirectionalComponents);
    }

    [Test]
    public void GetByName_WhereNameDoesntExist_Returns404()
    {
      var lException = Assert.Throws<HttpResponseException>(() => mController.Get("TotallyNonExistantScene"));
      Assert.AreEqual(HttpStatusCode.NotFound, lException.Response.StatusCode);
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
