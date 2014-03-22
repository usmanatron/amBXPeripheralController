using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Defaults;
using aPC.Server;
using NUnit.Framework;
using System.Threading;
using aPC.Common.Entities;

namespace aPC.Server.Tests
{
  [TestFixture]
  class ConductorManagerTests
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
      Thread.Sleep(200);

      Assert.IsTrue(mConductorManager.FrameConductor.IsRunning);
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
      Thread.Sleep(200);

      mConductorManager.LightConductors.ToList().ForEach(light => Assert.IsTrue(light.IsRunning));
      mConductorManager.FanConductors.ToList().ForEach(fan => Assert.IsTrue(fan.IsRunning));
      mConductorManager.RumbleConductors.ToList().ForEach(rumble => Assert.IsTrue(rumble.IsRunning));
    }

    private TestEngineManager mEngine;
    private TestConductorManager mConductorManager;
    private amBXScene mDifferentScene;
  }
}
