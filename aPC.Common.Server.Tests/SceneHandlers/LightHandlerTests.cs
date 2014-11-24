using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  internal class LightHandlerTests
  {
    private readonly eDirection[] directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene standardScene;
    private amBXScene nonLightScene;
    private Action action;

    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var nonLightframe = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(10000)
        .WithFanSection(DefaultFanSections.Full)
        .Build();

      nonLightScene = new amBXScene()
        {
          SceneType = eSceneType.Desync,
          IsExclusive = true,
          Frames = nonLightframe
        };

      standardScene = new DefaultScenes().Building;
      action = new Action(() => { });
    }

    [Test]
    public void NextLightSnapshot_IsAsExpected()
    {
      var handler = new ComponentHandler(eComponentType.Light, standardScene, action);

      var snapshot = handler.GetNextSnapshot(eDirection.North);
      var expectedFrame = standardScene.Frames[0];

      Assert.IsFalse(snapshot.IsComponentNull);
      Assert.AreEqual(expectedFrame.Lights.North, snapshot.Item);
      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
    }

    [Test]
    public void NextLightSnapshot_FromSceneWithoutLights_IsNull()
    {
      var handler = new ComponentHandler(eComponentType.Light, nonLightScene, action);

      var snapshot = handler.GetNextSnapshot(eDirection.North);
      var expectedFrame = nonLightScene.Frames[0];

      Assert.IsTrue(snapshot.IsComponentNull);
      Assert.IsNull(snapshot.Item);
      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
    }

    [Test]
    [TestCaseSource("directions")]
    public void NextLightSnapshot_ReturnsExpectedLight_DependantOnDirection(eDirection direction)
    {
      var handler = new ComponentHandler(eComponentType.Light, standardScene, action);

      var snapshot = handler.GetNextSnapshot(direction);
      var expectedFrame = standardScene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
      if (!snapshot.IsComponentNull)
      {
        Assert.AreEqual(expectedFrame.Lights.FadeTime, snapshot.FadeTime);
        Assert.AreEqual(expectedFrame.Lights.GetComponentValueInDirection(direction), snapshot.Item);
      }
    }
  }
}