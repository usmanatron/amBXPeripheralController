using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  internal class FrameHandlerTests
  {
    private readonly eDirection[] directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene scene;
    private Action action;

    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      scene = new DefaultScenes().Building;
      action = new Action(() => { });
    }

    [Test]
    public void NextFrame_IsAsExpected()
    {
      var handler = new FrameHandler(scene, action);

      var snapshot = handler.GetNextSnapshot(eDirection.North);
      var expectedFrame = scene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, snapshot.Frame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, snapshot.Frame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, snapshot.Frame.Lights);
    }

    [Test]
    [TestCaseSource("directions")]
    public void GetNextSnapshot_ReturnsExpectedFrame_RegardlessOfDirection(eDirection xiDirection)
    {
      var handler = new FrameHandler(scene, action);

      var snapshot = handler.GetNextSnapshot(xiDirection);
      var expectedFrame = scene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, snapshot.Frame.Length);
      Assert.AreEqual(expectedFrame.IsRepeated, snapshot.Frame.IsRepeated);
      Assert.AreEqual(expectedFrame.Lights, snapshot.Frame.Lights);
    }
  }
}