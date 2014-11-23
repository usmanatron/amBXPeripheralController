using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.Tests.Actors;
using aPC.Common.Server.Tests.SceneHandlers;
using NUnit.Framework;
using System;
using System.Diagnostics;

namespace aPC.Common.Server.Tests.Conductors
{
  [TestFixture]
  internal class ConductorBaseTests
  {
    private amBXScene initialScene;
    private TestSceneHandler handler;
    private TestActor actor;

    [SetUp]
    public void FixtureSetup()
    {
      initialScene = new amBXScene()
      {
        SceneType = eSceneType.Desync,
        IsExclusive = false
      };
      initialScene.Frames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(50)
        .WithLightSection(DefaultLightSections.JiraBlue)
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(50)
        .WithLightSection(DefaultLightSections.Blue)
        .Build();

      handler = new TestSceneHandler(initialScene, new Action(() => { }));
      actor = new TestActor(new TestEngineManager());
    }

    [Test]
    public void RunningOnce_ActsFrame()
    {
      var conductor = new TestConductor(eDirection.Everywhere, actor, handler);
      conductor.RunOnce();

      Assert.AreEqual(1, actor.TimesInvoked);
    }

    [Test]
    public void RunningOnce_AdvancesScene()
    {
      var conductor = new TestConductor(eDirection.Everywhere, actor, handler);
      conductor.RunOnce();

      Assert.AreEqual(initialScene.Frames[1], handler.NextFrame);
    }

    [Test]
    public void RunningOnce_WaitsForSceneLength()
    {
      var conductor = new TestConductor(eDirection.Everywhere, actor, handler);
      var stopwatch = new Stopwatch();
      var firstFrameLength = initialScene.Frames[0].Length;

      stopwatch.Start();
      conductor.RunOnce();
      stopwatch.Stop();

      Assert.GreaterOrEqual(stopwatch.ElapsedMilliseconds, firstFrameLength - 5);
      Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, firstFrameLength + 5);
    }

    [Test]
    public void Running_WithDormantHandler_ReturnsImmediately()
    {
      handler.IsEnabled = false;
      var conductor = new TestConductor(eDirection.Everywhere, actor, handler);
      var stopwatch = new Stopwatch();

      stopwatch.Start();
      conductor.Run();
      stopwatch.Stop();

      Assert.LessOrEqual(stopwatch.ElapsedMilliseconds, 50);
    }

    [Test]
    public void UpdateScene_UpdatesHandler()
    {
      var conductor = new TestConductor(eDirection.Everywhere, actor, handler);
      var newScene = new DefaultScenes().BuildBroken;

      conductor.UpdateScene(newScene);

      Assert.AreEqual(newScene, handler.Scene);
    }
  }
}