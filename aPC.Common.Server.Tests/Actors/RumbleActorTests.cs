using System;
using aPC.Common;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using aPC.Common.Server.Actors;
using aPC.Common.Server.Snapshots;
using NUnit.Framework;

namespace aPC.Common.Server.Tests.Actors
{
  [TestFixture]
  class RumbleActorTests
  {
    [SetUp]
    public void Setup()
    {
      mEngine = new TestEngineManager();
      mActor = new RumbleActor(mEngine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var lSnapshot = new ComponentSnapshot<Rumble>(1000);

      mActor.ActNextFrame(eDirection.Center, lSnapshot);

      Assert.IsNull(mEngine.Status.Rumbles.Rumble);
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponent()
    {
      var lSnapshot = new ComponentSnapshot<Rumble>(DefaultRumbleSections.Boing.Rumble, 100, 1000);

      mActor.ActNextFrame(eDirection.Center, lSnapshot);

      // Unlike other component tests, direction is not relevant as
      // only one rumble is currently supported.
      Assert.AreEqual(DefaultRumbleSections.Boing.Rumble, mEngine.Status.Rumbles.Rumble);
    }

    private TestEngineManager mEngine;
    private RumbleActor mActor;
  }
}
