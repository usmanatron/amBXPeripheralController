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

      actor.ActNextFrame(eDirection.East, snapshot);

      Assert.IsNull(engine.Status.FanSection.GetComponentValueInDirection(eDirection.East));
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponentInCorrectDirection()
    {
      var snapshot = new ComponentSnapshot(DefaultFans.FullPower, 1000);

      actor.ActNextFrame(eDirection.West, snapshot);

      foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
      {
        if (direction == eDirection.West || direction == eDirection.NorthWest)
        {
          Assert.AreEqual(DefaultFans.FullPower, engine.Status.FanSection.GetComponentValueInDirection(direction));
        }
        else
        {
          Assert.IsNull(engine.Status.FanSection.GetComponentValueInDirection(direction));
        }
      }
    }
  }
}