using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests.SceneHandlers
{
  [TestFixture]
  internal class FanHandlerTests
  {
    private readonly eDirection[] directions = (eDirection[])Enum.GetValues(typeof(eDirection));
    private amBXScene standardScene;
    private amBXScene nonFanScene;
    private Action action;

    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      var nonFanFrame = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithFrameLength(10000)
        .WithLightSection(DefaultLightSections.JiraBlue)
        .Build();

      nonFanScene = new amBXScene()
      {
        SceneType = eSceneType.Desync,
        IsExclusive = true,
        Frames = nonFanFrame
      };

      standardScene = new DefaultScenes().QuarterFans;
      action = new Action(() => { });
    }

    [Test]
    public void NextFanSnapshot_IsAsExpected()
    {
      var handler = new FanHandler(standardScene, action);

      var snapshot = handler.GetNextSnapshot(eDirection.East);
      var expectedFrame = standardScene.Frames[0];

      Assert.IsFalse(snapshot.IsComponentNull);
      Assert.AreEqual(expectedFrame.Fans.East, snapshot.Item);
      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
    }

    [Test]
    public void NextFanSnapshot_FromSceneWithoutFans_IsNull()
    {
      var handler = new FanHandler(nonFanScene, action);

      var snapshot = handler.GetNextSnapshot(eDirection.West);
      var expectedFrame = nonFanScene.Frames[0];

      Assert.IsTrue(snapshot.IsComponentNull);
      Assert.IsNull(snapshot.Item);
      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
    }

    [Test]
    [TestCaseSource("directions")]
    public void NextFanSnapshot_ReturnsExpectedFan_DependantOnDirection(eDirection direction)
    {
      var handler = new FanHandler(standardScene, action);

      var snapshot = handler.GetNextSnapshot(direction);
      var expectedFrame = standardScene.Frames[0];

      Assert.AreEqual(expectedFrame.Length, snapshot.Length);
      if (!snapshot.IsComponentNull)
      {
        Assert.AreEqual(expectedFrame.Fans.FadeTime, snapshot.FadeTime);
        Assert.AreEqual(expectedFrame.Fans.GetComponentValueInDirection(direction), snapshot.Item);
      }
    }
  }
}