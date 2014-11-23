using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.Tests;
using NUnit.Framework;
using System.Linq;
using System.Threading;

namespace aPC.Server.Tests
{
  [TestFixture]
  internal class ConductorManagerTests
  {
    [SetUp]
    public void Setup()
    {
      mEngine = new TestEngineManager();
      var lDefaultScenes = new DefaultScenes();

      var lScene = lDefaultScenes.Building;
      mDifferentScene = lDefaultScenes.BuildBrokenAndBuilding;
      mConductorManager = new TestConductorManager(mEngine, lScene);
    }

    [Test]
    public void EnablingSync_SetsFrameConductorRunning()
    {
      mConductorManager.EnableSync();
      Thread.Sleep(400);

      Assert.IsTrue(mConductorManager.FrameConductor.IsRunning.Get);
    }

    [Test]
    public void UpdateSync_UpdatesSceneInFrameConductor()
    {
      mConductorManager.UpdateSync(mDifferentScene);
    }

    [Test]
    public void EnablingDesync_SetsDesyncConductorsRunning()
    {
      mConductorManager.EnableDesync();
      Thread.Sleep(400);

      mConductorManager.LightConductors.ToList().ForEach(light => Assert.IsTrue(light.IsRunning.Get));
      mConductorManager.FanConductors.ToList().ForEach(fan => Assert.IsTrue(fan.IsRunning.Get));
      mConductorManager.RumbleConductors.ToList().ForEach(rumble => Assert.IsTrue(rumble.IsRunning.Get));
    }

    private TestEngineManager mEngine;
    private TestConductorManager mConductorManager;
    private amBXScene mDifferentScene;
  }
}