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
    private readonly eDirection[] directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene standardScene;
    private amBXScene nonRumbleScene;
    private Action action;

    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var rumbleFrame = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(10000)
        .WithRumbleSection(DefaultRumbleSections.SoftThunder)
        .WithLightSection(DefaultLightSections.JiraBlue)
        .Build();

      standardScene = new amBXScene()
      {
        SceneType = eSceneType.Desync,
        IsExclusive = true,
        Frames = rumbleFrame
      };

      nonRumbleScene = new DefaultScenes().Building;
      action = new Action(() => { });
    }

    [Test]
    public void NextRumbleSnapshot_IsAsExpected()
    {
      var handler = new RumbleHandler(standardScene, action);

      var snapshot = handler.GetNextSnapshot(eDirection.Center);
      var expectedFrame = standardScene.Frames[0];

      Assert.IsFalse(snapshot.IsComponentNull);
      Assert.AreEqual(expectedFrame.Rumbles.Rumble, snapshot.Item);
      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
    }

    [Test]
    public void NextRumbleSnapshot_FromSceneWithoutRumbles_IsNull()
    {
      var handler = new RumbleHandler(nonRumbleScene, action);

      var snapshot = handler.GetNextSnapshot(eDirection.West);
      var expectedFrame = nonRumbleScene.Frames[0];

      Assert.IsTrue(snapshot.IsComponentNull);
      Assert.IsNull(snapshot.Item);
      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
    }

    [Test]
    [TestCaseSource("Directions")]
    public void NextRumbleSnapshot_ReturnsARumble_IrrespectiveOfDirection(eDirection xiDirection)
    {
      var handler = new RumbleHandler(standardScene, action);

      var snapshot = handler.GetNextSnapshot(xiDirection);
      var expectedFrame = standardScene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
      Assert.IsFalse(snapshot.IsComponentNull);
    }
  }
}