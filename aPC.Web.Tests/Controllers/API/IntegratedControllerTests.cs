using aPC.Common;
using aPC.Common.Client.Tests.Communication;
using aPC.Common.Defaults;
using aPC.Web.Controllers.API;
using NUnit.Framework;
using System.Linq;
using System.Net;
using System.Web.Http;

namespace aPC.Web.Tests.Controllers.API
{
  [TestFixture]
  public class IntegratedControllerTests
  {
    private TestNotificationClient client;
    private IntegratedController controller;

    [SetUp]
    public void Setup()
    {
      client = new TestNotificationClient();
      controller = new IntegratedController(client);
    }

    [Test]
    public void GetWithoutName_ReturnsAllIntegratedScenes()
    {
      var integratedScenes = new SceneAccessor(new DefaultScenes()).GetAllScenes();

      var results = controller.Get();

      Assert.AreEqual(integratedScenes.Count, results.Count());
      CollectionAssert.AreEquivalent(integratedScenes.Select(scene => scene.Key), results.Select(scene => scene.SceneName));
    }

    [Test]
    public void GetByName_ReturnsExpectedScene()
    {
      var scene = new SceneAccessor(new DefaultScenes()).GetScene("poolq2_event");

      var result = controller.Get("poolq2_event");

      Assert.AreEqual(scene.SceneType, result.SceneType);
      Assert.AreEqual(scene.FrameStatistics.SceneLength, result.FrameStatistics.SceneLength);
      Assert.AreEqual(scene.FrameStatistics.EnabledDirectionalComponents, result.FrameStatistics.EnabledDirectionalComponents);
    }

    [Test]
    public void GetByName_WhereNameDoesntExist_Returns404()
    {
      var exception = Assert.Throws<HttpResponseException>(() => controller.Get("TotallyNonExistantScene"));
      Assert.AreEqual(HttpStatusCode.NotFound, exception.Response.StatusCode);
    }

    [Test]
    public void Post_SendsExpectedScene()
    {
      controller.Post("poolq2_event");

      Assert.AreEqual(1, client.NumberOfIntegratedScenesPushed);
      Assert.AreEqual(0, client.NumberOfCustomScenesPushed);
      Assert.AreEqual("poolq2_event", client.IntegratedScenesPushed.First());
    }
  }
}