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
    private FanActor actor;

    [SetUp]
    public void Setup()
    {
      engine = new TestEngineManager();
      actor = new FanActor(engine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var snapshot = new ComponentSnapshot<Fan>(1000);

      actor.ActNextFrame(eDirection.East, snapshot);

      Assert.IsNull(engine.Status.Fans.East);
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponentInCorrectDirection()
    {
      var snapshot = new ComponentSnapshot<Fan>(DefaultFans.FullPower, 100, 1000);

      actor.ActNextFrame(eDirection.West, snapshot);

      foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
      {
        if (direction == eDirection.West || direction == eDirection.NorthWest)
        {
          Assert.AreEqual(DefaultFans.FullPower, engine.Status.Fans.GetComponentValueInDirection(direction));
        }
        else
        {
          Assert.IsNull(engine.Status.Fans.GetComponentValueInDirection(direction));
        }
      }
    }
  }
}