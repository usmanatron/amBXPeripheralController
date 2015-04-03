using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Snapshots;
using NUnit.Framework;
using System;

namespace aPC.Common.Server.Tests.Actors
{
  [TestFixture]
  internal class LightActorTests
  {
    private TestEngineManager engine;
    private ComponentActor actor;

    [SetUp]
    public void Setup()
    {
      engine = new TestEngineManager();
      actor = new ComponentActor(eComponentType.Light, engine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var snapshot = new ComponentSnapshot(1000);

      actor.ActNextFrame(eDirection.North, snapshot);

      Assert.IsNull(engine.Status.LightSection.GetComponentValueInDirection(eDirection.North));
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponentInCorrectDirection()
    {
      var snapshot = new ComponentSnapshot(DefaultLights.Blue, 1000);

      actor.ActNextFrame(eDirection.East, snapshot);

      foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
      {
        if (direction == eDirection.East)
        {
          Assert.AreEqual(DefaultLights.Blue, engine.Status.LightSection.GetComponentValueInDirection(direction));
        }
        else
        {
          Assert.IsNull(engine.Status.LightSection.GetComponentValueInDirection(direction));
        }
      }
    }
  }
}