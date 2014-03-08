using System.Diagnostics;
using NUnit.Framework;
using aPC.Common.Defaults;
using aPC.Common.Server.Tests;
using aPC.Common.Server.Tests.EngineActors;
using aPC.Common.Server.Tests.SceneHandlers;
using aPC.Common.Entities;
using aPC.Common.Builders;

namespace aPC.Common.Server.Tests.Conductors
{
  [TestFixture]
  class ConductorBaseTests
  {
    [SetUp]
    public void FixtureSetup()
    {
      mInitialScene = new amBXScene()
      {
        IsEvent = false,
        IsSynchronised = false,
        IsExclusive = false
      };
      mInitialScene.Frames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(50)
        .WithLightSection(DefaultLightSections.JiraBlue)
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(50)
        .WithLightSection(DefaultLightSections.Blue)
        .Build();
        
      mHandler = new TestSceneHandler(mInitialScene);
      mActor = new TestEngineActor(new TestEngineManager());
    }

    [Test]
    public void RunningOnce_ActsFrame()
    {
      var lConductor = new TestConductor(eDirection.Everywhere, mActor, mHandler);
      lConductor.Run();

      Assert.AreEqual(1, mActor.TimesInvoked);
    }

    [Test]
    public void RunningOnce_AdvancesScene()
    {
      var lConductor = new TestConductor(eDirection.Everywhere, mActor, mHandler);
      lConductor.Run();

      Assert.AreEqual(mInitialScene.Frames[1], mHandler.NextFrame);
    }

    [Test]
    public void RunningOnce_WaitsForSceneLength()
    {
      var lConductor = new TestConductor(eDirection.Everywhere, mActor, mHandler);
      var lStopwatch = new Stopwatch();
      var lFirstFrameLength = mInitialScene.Frames[0].Length;

      lStopwatch.Start();
      lConductor.Run();
      lStopwatch.Stop();

      Assert.GreaterOrEqual(lStopwatch.ElapsedMilliseconds, lFirstFrameLength);
      Assert.LessOrEqual(lStopwatch.ElapsedMilliseconds, lFirstFrameLength + 50);
    }

    [Test]
    public void RunningOnce_WithDormantHandler_WaitsForASecond()
    {
      mHandler.IsEnabled = false;
      var lConductor = new TestConductor(eDirection.Everywhere, mActor, mHandler);
      var lStopwatch = new Stopwatch();

      lStopwatch.Start();
      lConductor.Run();
      lStopwatch.Stop();

      Assert.GreaterOrEqual(lStopwatch.ElapsedMilliseconds, 1000);
      Assert.LessOrEqual(lStopwatch.ElapsedMilliseconds, 1000 + 50);
    }

    [Test]
    public void UpdateScene_UpdatesHandler()
    {
      var lConductor = new TestConductor(eDirection.Everywhere, mActor, mHandler);
      var lNewScene = new DefaultScenes().BuildBroken;

      lConductor.UpdateScene(lNewScene);

      Assert.AreEqual(lNewScene, mHandler.Scene);
    }

    private amBXScene mInitialScene;
    private TestSceneHandler mHandler;
    private TestEngineActor mActor;
  }
}
