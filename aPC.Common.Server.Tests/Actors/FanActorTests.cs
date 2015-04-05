using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Snapshots;
using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests.Actors
{
  [TestFixture]
  internal class FanActorTests
  {
    private TestEngineManager engine;
    private ComponentActor actor;

    [SetUp]
    public void Setup()
    {
      engine = new TestEngineManager();
      actor = new ComponentActor(eComponentType.Fan, engine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var snapshot = new ComponentSnapshot(1000);

      actor.ActNextFrame(snapshot);

      Assert.IsNull(engine.Status.FanSection.GetComponentSectionInDirection(eDirection.East));
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponentInCorrectDirection()
    {
      var fan = DefaultFans.FullPower;
      fan.Direction = eDirection.West;
      var snapshot = new ComponentSnapshot(fan, 1000);

      actor.ActNextFrame(snapshot);

      foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
      {
        if (direction == eDirection.West)
        {
          Assert.AreEqual(DefaultFans.FullPower, engine.Status.FanSection.GetComponentSectionInDirection(direction));
        }
        else
        {
          Assert.IsNull(engine.Status.FanSection.GetComponentSectionInDirection(direction));
        }
      }
    }
  }
}