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
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      mScene = new DefaultScenes().Building;
      mAction = new Action(() => { });
    }

    [Test]
    public void NextFrame_IsAsExpected()
    {
      var lHandler = new FrameHandler(mScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(eDirection.North);
      var lExpectedFrame = mScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Frame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lSnapshot.Frame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lSnapshot.Frame.Lights);
    }

    [Test]
    [TestCaseSource("Directions")]
    public void GetNextSnapshot_ReturnsExpectedFrame_RegardlessOfDirection(eDirection xiDirection)
    {
      var lHandler = new FrameHandler(mScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(xiDirection);
      var lExpectedFrame = mScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Frame.Length);
      Assert.AreEqual(lExpectedFrame.IsRepeated, lSnapshot.Frame.IsRepeated);
      Assert.AreEqual(lExpectedFrame.Lights, lSnapshot.Frame.Lights);
    }

    private readonly eDirection[] Directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene mScene;
    private Action mAction;
  }
}