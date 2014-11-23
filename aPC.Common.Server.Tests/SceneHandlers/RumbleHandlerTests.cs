using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  internal class RumbleHandlerTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var lRumbleFrame = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(10000)
        .WithRumbleSection(DefaultRumbleSections.SoftThunder)
        .WithLightSection(DefaultLightSections.JiraBlue)
        .Build();

      mStandardScene = new amBXScene()
      {
        SceneType = eSceneType.Desync,
        IsExclusive = true,
        Frames = lRumbleFrame
      };

      mNonRumbleScene = new DefaultScenes().Building;
      mAction = new Action(() => { });
    }

    [Test]
    public void NextRumbleSnapshot_IsAsExpected()
    {
      var lHandler = new RumbleHandler(mStandardScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(eDirection.Center);
      var lExpectedFrame = mStandardScene.Frames[0];

      Assert.IsFalse(lSnapshot.IsComponentNull);
      Assert.AreEqual(lExpectedFrame.Rumbles.Rumble, lSnapshot.Item);
      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
    }

    [Test]
    public void NextRumbleSnapshot_FromSceneWithoutRumbles_IsNull()
    {
      var lHandler = new RumbleHandler(mNonRumbleScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(eDirection.West);
      var lExpectedFrame = mNonRumbleScene.Frames[0];

      Assert.IsTrue(lSnapshot.IsComponentNull);
      Assert.IsNull(lSnapshot.Item);
      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
    }

    [Test]
    [TestCaseSource("Directions")]
    public void NextRumbleSnapshot_ReturnsARumble_IrrespectiveOfDirection(eDirection xiDirection)
    {
      var lHandler = new RumbleHandler(mStandardScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(xiDirection);
      var lExpectedFrame = mStandardScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
      Assert.IsFalse(lSnapshot.IsComponentNull);
    }

    private readonly eDirection[] Directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene mStandardScene;
    private amBXScene mNonRumbleScene;
    private Action mAction;
  }
}