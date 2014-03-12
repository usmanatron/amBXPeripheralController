using aPC.Common;
using System;
using System.Linq;
using NUnit.Framework;
using aPC.Server.EngineActors;
using aPC.Server.Snapshots;
using aPC.Common.Builders;
using aPC.Common.Defaults;

namespace aPC.Server.Tests.EngineActors
{
  [TestFixture]
  class FrameActorTests
  {
    [SetUp]
    public void Setup()
    {
      mEngine = new TestEngineManager();
      mActor = new FrameActor(mEngine);
    }

    [Test]
    public void UpdatingOneLight_SuccessfullyUpdated()
    {
      var lLightSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.North, DefaultLights.Green)
        .Build();

      var lFrame = GetFrameBuilderWithDefaultFrame()
        .WithLightSection(lLightSection)
        .Build()
        .Single();

      mActor.ActNextFrame(eDirection.East, new FrameSnapshot(lFrame, 100));

      Assert.IsTrue(mEngine.Updated[eComponentType.Light]);
      Assert.IsFalse(mEngine.Updated[eComponentType.Fan]);
      Assert.IsFalse(mEngine.Updated[eComponentType.Rumble]);
      Assert.AreEqual(DefaultLights.Green, mEngine.Status.Lights.North);
    }

    [Test]
    public void UpdatingOneFan_SuccessfullyUpdated()
    {
      var lFanSection = new FanSectionBuilder()
        .WithFadeTime(100)
        .WithFanInDirection(eDirection.West, DefaultFans.QuarterPower)
        .Build();

      var lFrame = GetFrameBuilderWithDefaultFrame()
        .WithFanSection(lFanSection)
        .Build()
        .Single();

      mActor.ActNextFrame(eDirection.East, new FrameSnapshot(lFrame, 100));

      Assert.IsFalse(mEngine.Updated[eComponentType.Light]);
      Assert.IsTrue(mEngine.Updated[eComponentType.Fan]);
      Assert.IsFalse(mEngine.Updated[eComponentType.Rumble]);
      Assert.AreEqual(DefaultFans.QuarterPower, mEngine.Status.Fans.West);
    }

    [Test]
    public void UpdatingRumble_SuccessfullyUpdated()
    {
      var lFrame = GetFrameBuilderWithDefaultFrame()
        .WithRumbleSection(DefaultRumbleSections.SoftThunder)
        .Build()
        .Single();

      mActor.ActNextFrame(eDirection.Center, new FrameSnapshot(lFrame, 100));

      Assert.IsFalse(mEngine.Updated[eComponentType.Light]);
      Assert.IsFalse(mEngine.Updated[eComponentType.Fan]);
      Assert.IsTrue(mEngine.Updated[eComponentType.Rumble]);
      Assert.AreEqual(DefaultRumbleSections.SoftThunder.Rumble, mEngine.Status.Rumbles.Rumble);
    }

    [Test]
    public void WhenActingNextFrame_DirectionIsIgnored()
    {
      var lLightSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.North, DefaultLights.Green)
        .Build();

      var lFrame = GetFrameBuilderWithDefaultFrame()
        .WithLightSection(lLightSection)
        .Build()
        .Single();
      
      foreach (eDirection lDirection in Enum.GetValues(typeof(eDirection)))
      {
        mActor.ActNextFrame(lDirection, new FrameSnapshot(lFrame, 100));
      
        Assert.IsTrue(mEngine.Updated[eComponentType.Light]);
        Assert.IsFalse(mEngine.Updated[eComponentType.Fan]);
        Assert.IsFalse(mEngine.Updated[eComponentType.Rumble]);
        Assert.AreEqual(DefaultLights.Green, mEngine.Status.Lights.North);
      }
    }

    private FrameBuilder GetFrameBuilderWithDefaultFrame()
    {
      return new FrameBuilder()
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(true);
    }

    private TestEngineManager mEngine;
    private FrameActor mActor;
  }
}
