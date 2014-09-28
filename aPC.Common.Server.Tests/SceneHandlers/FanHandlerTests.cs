using System;
using NUnit.Framework;
using aPC.Common.Server.SceneHandlers;
using aPC.Common;
using aPC.Common.Defaults;
using aPC.Common.Builders;
using aPC.Common.Entities;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  class FanHandlerTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var lNonFanFrame = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(10000)
        .WithLightSection(DefaultLightSections.JiraBlue)
        .Build();

      mNonFanScene = new amBXScene()
      {
        SceneType = eSceneType.Desync,
        IsExclusive = true,
        Frames = lNonFanFrame
      };

      mStandardScene = new DefaultScenes().QuarterFans;
      mAction = new Action(() => { });
    }

    [Test]
    public void NextFanSnapshot_IsAsExpected()
    {
      var lHandler = new FanHandler(mStandardScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(eDirection.East);
      var lExpectedFrame = mStandardScene.Frames[0];

      Assert.IsFalse(lSnapshot.IsComponentNull);
      Assert.AreEqual(lExpectedFrame.Fans.East, lSnapshot.Item);
      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
    }

    [Test]
    public void NextFanSnapshot_FromSceneWithoutFans_IsNull()
    {
      var lHandler = new FanHandler(mNonFanScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(eDirection.West);
      var lExpectedFrame = mNonFanScene.Frames[0];

      Assert.IsTrue(lSnapshot.IsComponentNull);
      Assert.IsNull(lSnapshot.Item);
      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
    }

    [Test]
    [TestCaseSource("Directions")]
    public void NextFanSnapshot_ReturnsExpectedFan_DependantOnDirection(eDirection xiDirection)
    {
      var lHandler = new FanHandler(mStandardScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(xiDirection);
      var lExpectedFrame = mStandardScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
      if (!lSnapshot.IsComponentNull)
      {
        Assert.AreEqual(lExpectedFrame.Fans.FadeTime, lSnapshot.FadeTime);
        Assert.AreEqual(lExpectedFrame.Fans.GetComponentValueInDirection(xiDirection), lSnapshot.Item);
      }
    }

    private readonly eDirection[] Directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene mStandardScene;
    private amBXScene mNonFanScene;
    private Action mAction;
  }
}