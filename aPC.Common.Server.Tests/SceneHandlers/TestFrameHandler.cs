using NUnit.Framework;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Entities;
using aPC.Common.Defaults;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  class TestFrameHandler
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      mScene = new DefaultScenes().Building;
    }

    [Test]
    public void NextFrame_IsAsExpected()
    {
      var lHandler = new FrameHandler(mScene);
      
      var lSnapshot = lHandler.GetNextSnapshot(eDirection.North);
      var lExpectedFrame = mScene.Frames[0];

      Assert.AreEqual(lSnapshot.Frame.Length, lExpectedFrame.Length);
      Assert.AreEqual(lSnapshot.Frame.IsRepeated, lExpectedFrame.IsRepeated);
      Assert.AreEqual(lSnapshot.Frame.Lights, lExpectedFrame.Lights);
    }

    [Test]
    [TestCaseSource("Directions")]
    public void GetNextSnapshot_ReturnsExpectedFrame_RegardlessOfDirection(eDirection xiDirection)
    {
      var lHandler = new FrameHandler(mScene);

      var lSnapshot = lHandler.GetNextSnapshot(xiDirection);
      var lExpectedFrame = mScene.Frames[0];

      Assert.AreEqual(lSnapshot.Frame.Length, lExpectedFrame.Length);
      Assert.AreEqual(lSnapshot.Frame.IsRepeated, lExpectedFrame.IsRepeated);
      Assert.AreEqual(lSnapshot.Frame.Lights, lExpectedFrame.Lights);
    }


    private readonly eDirection[] Directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene mScene;
  }
}
