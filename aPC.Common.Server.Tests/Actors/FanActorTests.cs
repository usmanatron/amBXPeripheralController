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
  class FanActorTests
  {
    [SetUp]
    public void Setup()
    {
      mEngine = new TestEngineManager();
      mActor = new FanActor(mEngine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var lSnapshot = new ComponentSnapshot<Fan>(1000);

      mActor.ActNextFrame(eDirection.East, lSnapshot);

      Assert.IsNull(mEngine.Status.Fans.East);
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponentInCorrectDirection()
    {
      var lSnapshot = new ComponentSnapshot<Fan>(DefaultFans.FullPower, 100, 1000);

      mActor.ActNextFrame(eDirection.West, lSnapshot);

      foreach (eDirection lDirection in Enum.GetValues(typeof(eDirection)))
      {
        if (lDirection == eDirection.West ||
            lDirection == eDirection.NorthWest)
        {
          Assert.AreEqual(DefaultFans.FullPower, mEngine.Status.Fans.GetComponentValueInDirection(lDirection));
        }
        else
        {
          Assert.IsNull(mEngine.Status.Fans.GetComponentValueInDirection(lDirection));
        }
      }
    }

    private TestEngineManager mEngine;
    private FanActor mActor;
  }
}
