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
    private ComponentActor<Light> actor;

    [SetUp]
    public void Setup()
    {
      engine = new TestEngineManager();
      actor = new ComponentActor<Light>(engine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var snapshot = new ComponentSnapshot<Light>(1000);

      actor.ActNextFrame(eDirection.North, snapshot);

      Assert.IsNull(engine.Status.Lights.North);
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponentInCorrectDirection()
    {
      var snapshot = new ComponentSnapshot<Light>(DefaultLights.Blue, 100, 1000);

      actor.ActNextFrame(eDirection.East, snapshot);

      foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
      {
        if (direction == eDirection.East)
        {
          Assert.AreEqual(DefaultLights.Blue, engine.Status.Lights.GetComponentValueInDirection(direction));
        }
        else
        {
          Assert.IsNull(engine.Status.Lights.GetComponentValueInDirection(direction));
        }
      }
    }
  }
}