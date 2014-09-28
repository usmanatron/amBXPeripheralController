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
  class LightHandlerTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var lNonLightframe  = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(10000)
        .WithFanSection(DefaultFanSections.Full)
        .Build();

      mNonLightScene = new amBXScene()
        {
          SceneType = eSceneType.Desync,
          IsExclusive = true,
          Frames = lNonLightframe
        };

      mStandardScene = new DefaultScenes().Building;
      mAction = new Action(() => { });
    }

    [Test]
    public void NextLightSnapshot_IsAsExpected()
    {
      var lHandler = new LightHandler(mStandardScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(eDirection.North);
      var lExpectedFrame = mStandardScene.Frames[0];

      Assert.IsFalse(lSnapshot.IsComponentNull);
      Assert.AreEqual(lExpectedFrame.Lights.North, lSnapshot.Item);
      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
    }

    [Test]
    public void NextLightSnapshot_FromSceneWithoutLights_IsNull()
    {
      var lHandler = new LightHandler(mNonLightScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(eDirection.North);
      var lExpectedFrame = mNonLightScene.Frames[0];

      Assert.IsTrue(lSnapshot.IsComponentNull);
      Assert.IsNull(lSnapshot.Item);
      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
    }

    [Test]
    [TestCaseSource("Directions")]
    public void NextLightSnapshot_ReturnsExpectedLight_DependantOnDirection(eDirection xiDirection)
    {
      var lHandler = new LightHandler(mStandardScene, mAction);

      var lSnapshot = lHandler.GetNextSnapshot(xiDirection);
      var lExpectedFrame = mStandardScene.Frames[0];

      Assert.AreEqual(lExpectedFrame.Length, lSnapshot.Length);
      if (!lSnapshot.IsComponentNull)
      {
        Assert.AreEqual(lExpectedFrame.Lights.FadeTime, lSnapshot.FadeTime);
        Assert.AreEqual(lExpectedFrame.Lights.GetComponentValueInDirection(xiDirection), lSnapshot.Item);
      }
    }

    private readonly eDirection[] Directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene mStandardScene;
    private amBXScene mNonLightScene;
    private Action mAction;
  }
}
