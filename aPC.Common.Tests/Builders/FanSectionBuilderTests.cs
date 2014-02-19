using System;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  class FanSectionBuilderTests
  {
    [Test]
    public void NewFanSection_WithNoFadeTime_ThrowsException()
    {
      var lSectionBuilder = new FanSectionBuilder()
        .WithAllFans(mFull);

      Assert.Throws<ArgumentException>(() => lSectionBuilder.Build());
    }

    [Test]
    public void NewFanSection_WithNoFans_ThrowsException()
    {
      var lSectionBuilder = new FanSectionBuilder()
        .WithFadeTime(100);

      Assert.Throws<ArgumentException>(() => lSectionBuilder.Build());
    }

    [Test]
    public void TryingtoSpecifyAFanInAnUnsupportedDirection_ThrowsException()
    {
      var lSectionBuilder = new FanSectionBuilder();
      Assert.Throws<InvalidOperationException>(() => lSectionBuilder.WithFanInDirection(eDirection.South, mFull));
    }

    [Test]
    public void NewFanSection_CanUpdateAllFansAtSametime()
    {
      var lSection = new FanSectionBuilder()
        .WithFadeTime(100)
        .WithAllFans(mHalf)
        .Build();

      Assert.AreEqual(mHalf, lSection.East);
      Assert.AreEqual(mHalf, lSection.West);
    }

    [Test]
    public void FanSection_WithDifferentFanTypesOnEachFan_CorrectlySpecified()
    {
      var lSection = new FanSectionBuilder()
        .WithFadeTime(100)
        .WithFanInDirection(eDirection.East, mFull)
        .WithFanInDirection(eDirection.West, mHalf)
        .Build();

      Assert.AreEqual(mFull, lSection.East);
      Assert.AreEqual(mHalf, lSection.West);
    }

    [Test]
    [TestCase(eDirection.East, eDirection.NorthEast)]
    [TestCase(eDirection.West, eDirection.NorthWest)]
    public void SpecifyingFanRepeatedly_InComplimentaryDirections_UsesTheLastAssignment(eDirection xiFirst, eDirection xiSecond)
    {
      var lSection = new FanSectionBuilder()
        .WithFadeTime(100)
        .WithFanInDirection(xiFirst, mFull)
        .WithFanInDirection(xiSecond, mHalf)
        .Build();

      Assert.AreEqual(mHalf, GetFanInDirection(lSection, xiSecond));
    }

    private Fan GetFanInDirection(FanSection xiFanSection, eDirection xiDirection)
    {
      switch (xiDirection)
      {
        case eDirection.East:
        case eDirection.NorthEast:
          return xiFanSection.East;
        case eDirection.West:
        case eDirection.NorthWest:
          return xiFanSection.West;
        default:
          throw new InvalidOperationException("Unexpected Direction");
      }
    }

    private readonly Fan mFull = DefaultFans.FullPower;
    private readonly Fan mHalf = DefaultFans.HalfPower;
  }
}
