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
  class LightActorTests
  {
    [SetUp]
    public void Setup()
    {
      mEngine = new TestEngineManager();
      mActor = new LightActor(mEngine);
    }

    [Test]
    public void ActingNextSnapshot_WithNullComponent_DoesNothing()
    {
      var lSnapshot = new ComponentSnapshot<Light>(1000);

      mActor.ActNextFrame(eDirection.North, lSnapshot);

      Assert.IsNull(mEngine.Status.Lights.North);
    }

    [Test]
    public void ActingNextSnapshot_UpdatesComponentInCorrectDirection()
    {
      var lSnapshot = new ComponentSnapshot<Light>(DefaultLights.Blue, 100, 1000);

      mActor.ActNextFrame(eDirection.East, lSnapshot);

      foreach (eDirection lDirection in Enum.GetValues(typeof(eDirection)))
      {
        if (lDirection == eDirection.East)
        {
          Assert.AreEqual(DefaultLights.Blue, mEngine.Status.Lights.GetComponentValueInDirection(lDirection));
        }
        else
        {
          Assert.IsNull(mEngine.Status.Lights.GetComponentValueInDirection(lDirection));
        }
      }
    }

    private TestEngineManager mEngine;
    private LightActor mActor;
  }
}
