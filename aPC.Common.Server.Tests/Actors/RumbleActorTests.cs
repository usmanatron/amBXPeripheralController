using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Snapshots;
using NUnit.Framework;
using System.Linq;

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

      CollectionAssert.IsEmpty(engine.Status.RumbleSection.Rumbles);
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponent()
    {
      var snapshot = new ComponentSnapshot(DefaultRumbleSections.Boing.Rumbles.Single(), 1000);

      actor.ActNextFrame(eDirection.Center, snapshot);

      Assert.AreEqual(DefaultRumbleSections.Boing.Rumbles.Single(), engine.Status.RumbleSection.Rumbles.Single());
    }
  }
}