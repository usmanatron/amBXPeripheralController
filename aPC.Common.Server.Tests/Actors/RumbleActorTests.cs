using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Snapshots;
using NUnit.Framework;

namespace aPC.Common.Server.Tests.Actors
{
  [TestFixture]
  internal class RumbleActorTests
  {
    private TestEngineManager engine;
    private ComponentActor actor;

    [SetUp]
    public void Setup()
    {
      engine = new TestEngineManager();
      actor = new ComponentActor(eComponentType.Rumble, engine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var snapshot = new ComponentSnapshot(1000);

      actor.ActNextFrame(eDirection.Center, snapshot);

      Assert.IsNull(engine.Status.Rumbles.Rumble);
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponent()
    {
      var snapshot = new ComponentSnapshot(DefaultRumbleSections.Boing.Rumble, 100, 1000);

      actor.ActNextFrame(eDirection.Center, snapshot);

      // Unlike other component tests, direction is not relevant as
      // only one rumble is currently supported.
      Assert.AreEqual(DefaultRumbleSections.Boing.Rumble, engine.Status.Rumbles.Rumble);
    }
  }
}