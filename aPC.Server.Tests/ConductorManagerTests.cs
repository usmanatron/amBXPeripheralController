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
    private TestEngineManager engine;
    private TestConductorManager conductorManager;
    private amBXScene differentScene;

    [SetUp]
    public void Setup()
    {
      engine = new TestEngineManager();
      var defaultScenes = new DefaultScenes();

      var scene = defaultScenes.Building;
      differentScene = defaultScenes.BuildBrokenAndBuilding;
      conductorManager = new TestConductorManager(engine, scene);
    }

    [Test]
    public void EnablingSync_SetsFrameConductorRunning()
    {
      conductorManager.EnableSync();
      Thread.Sleep(400);

      Assert.IsTrue(conductorManager.FrameConductor.IsRunning.Get);
    }

    [Test]
    public void UpdateSync_UpdatesSceneInFrameConductor()
    {
      conductorManager.UpdateSync(differentScene);
    }

    [Test]
    public void EnablingDesync_SetsDesyncConductorsRunning()
    {
      conductorManager.EnableDesync();
      Thread.Sleep(400);

      conductorManager.LightConductors.ToList().ForEach(light => Assert.IsTrue(light.IsRunning.Get));
      conductorManager.FanConductors.ToList().ForEach(fan => Assert.IsTrue(fan.IsRunning.Get));
      conductorManager.RumbleConductors.ToList().ForEach(rumble => Assert.IsTrue(rumble.IsRunning.Get));
    }
  }
}