using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Defaults;
using aPC.Server;
using NUnit.Framework;
using System.Threading;

namespace aPC.Server.Tests
{
  [TestFixture]
  class ConductorManagerTests
  {
    [SetUp]
    public void Setup()
    {
      mEngine = new TestEngineManager();
      var lScene = new DefaultScenes().Building;
      mConductorManager = new TestConductorManager(mEngine, lScene);
    }

    [Test]
    public void EnablingSync_SetsFrameConductorRunning()
    {
      mConductorManager.EnableSync();
      Thread.Sleep(100);

      Assert.IsTrue(mConductorManager.FrameConductor.IsRunning);
    }

    private TestEngineManager mEngine;
    private TestConductorManager mConductorManager;
  }
}
