using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  internal class SceneHandlerBaseTests
  {
    private amBXScene initialScene;
    private amBXScene blueEvent;
    private amBXScene purpleEvent;
    private amBXScene unrepeatedScene;
    private Action action;

    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var initialSceneFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(false)
        .WithFrameLength(1000)
        .WithLightSection(DefaultLightSections.SoftPink)
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(2000)
        .WithLightSection(DefaultLightSections.Orange)
        .Build();

      var blueEventFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(500)
        .WithLightSection(DefaultLightSections.JiraBlue)
        .Build();

      var purpleEventFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(250)
        .WithLightSection(DefaultLightSections.StrongPurple)
        .Build();

      var unrepeatedFrames = new FrameBuilder()
        .AddFrame()
        .WithRepeated(false)
        .WithFrameLength(125)
        .WithLightSection(DefaultLightSections.Red)
        .Build();

      initialScene = new amBXScene
      {
        SceneType = eSceneType.Desync,
        IsExclusive = false,
        Frames = initialSceneFrames
      };

      blueEvent = new amBXScene
      {
        SceneType = eSceneType.Event,
        IsExclusive = false,
        Frames = blueEventFrames
      };

      purpleEvent = new amBXScene
      {
        SceneType = eSceneType.Event,
        IsExclusive = false,
        Frames = purpleEventFrames
      };

      unrepeatedScene = new amBXScene
      {
        SceneType = eSceneType.Desync,
        IsExclusive = false,
        Frames = unrepeatedFrames
      };

      action = new Action(() => { });
    }

    [Test]
    public void NewSceneHandler_IsDisabledByDefault()
    {
      var handler = new TestSceneHandler(initialScene, action);
      Assert.IsFalse(handler.IsEnabled);
    }

    [Test]
    public void NewSceneHandler_IsEnabledWhenDoneExplicitly()
    {
      var handler = new TestSceneHandler(initialScene, action);
      handler.Enable();
      Assert.IsTrue(handler.IsEnabled);
    }

    [Test]
    public void GetNextFrame_GetsExpectedData()
    {
      var handler = new TestSceneHandler(initialScene, action);

      var frame = handler.NextFrame;
      var expectedFrame = initialScene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, frame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, frame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, frame.Lights);
    }

    [Test]
    public void AdvancingHandlerOnFirstRun_GivesSecondFrame()
    {
      var handler = new TestSceneHandler(initialScene, action);

      handler.AdvanceScene();
      var frame = handler.NextFrame;
      var expectedFrame = initialScene.Frames[1];

      Assert.AreEqual(expectedFrame.Length, frame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, frame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, frame.Lights);
    }

    [Test]
    public void AdvancingHandlerThreeTimes_ReturnsToFirstRepeatableFrame()
    {
      var handler = new TestSceneHandler(initialScene, action);

      handler.AdvanceScene();
      handler.AdvanceScene();

      var frame = handler.NextFrame;
      // The first repeatable frame is the second one in the scene
      var expectedFrame = initialScene.Frames[1];

      Assert.AreEqual(expectedFrame.Length, frame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, frame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, frame.Lights);
    }

    [Test]
    public void AfterRunningThroughAnEvent_RevertsToPreviousScene()
    {
      var handler = new TestSceneHandler(initialScene, action);
      handler.UpdateScene(blueEvent);

      handler.AdvanceScene();
      var frame = handler.NextFrame;
      var expectedFrame = initialScene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, frame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, frame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, frame.Lights);
    }

    [Test]
    public void AfterRunningThroughAnEvent_WithASpecifiedAction_GivenActionIsRun()
    {
      var actionRun = false;
      var handler = new TestSceneHandler(initialScene, () => actionRun = true);

      handler.UpdateScene(blueEvent);
      handler.AdvanceScene();

      Assert.IsTrue(actionRun);
    }

    [Test]
    public void NoMoreApplicableScenes_MarksHandlerAsDormant()
    {
      var handler = new TestSceneHandler(unrepeatedScene, action);
      handler.AdvanceScene();

      var frame = handler.NextFrame;

      Assert.IsFalse(handler.IsEnabled);
      Assert.AreEqual(default(int), frame.Length);
      Assert.AreEqual(default(bool), frame.IsRepeated);
      Assert.IsNull(frame.Lights);
    }

    [Test]
    public void HandlerWithEvent_PushingAnotherEvent_DoesNotUpdatePreviousScene()
    {
      var handler = new TestSceneHandler(initialScene, action);
      handler.UpdateScene(blueEvent);
      handler.UpdateScene(purpleEvent);

      // Confirm it's actually updated the Scene Handler
      var eventFrame = handler.NextFrame;
      Assert.AreEqual(eventFrame.Lights, purpleEvent.Frames.Single().Lights);

      handler.AdvanceScene();

      // Confirm the previous scene is mInitialScene
      var previousFrame = handler.NextFrame;
      var expectedFrame = initialScene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, previousFrame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, previousFrame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, previousFrame.Lights);
    }

    [Test]
    public void HandlerWithEvent_PushingNonEvent_QuietlyUpdatesPreviousScene()
    {
      var handler = new TestSceneHandler(initialScene, action);
      handler.UpdateScene(blueEvent);
      handler.UpdateScene(unrepeatedScene);

      // Confirm the current Scene is still the event
      var firstFrame = handler.NextFrame;
      Assert.AreEqual(firstFrame.Lights, blueEvent.Frames.Single().Lights);

      handler.AdvanceScene();

      // Confirm the previous scene has changed
      var previousFrame = handler.NextFrame;
      var expectedFrame = unrepeatedScene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, previousFrame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, previousFrame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, previousFrame.Lights);
    }
  }
}