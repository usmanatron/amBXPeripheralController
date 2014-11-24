using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  internal class FanSectionBuilderTests
  {
    private readonly Fan fullPower = DefaultFans.FullPower;
    private readonly Fan halfPower = DefaultFans.HalfPower;

    [Test]
    public void NewFanSection_WithNoFadeTime_ThrowsException()
    {
      var sectionBuilder = new FanSectionBuilder()
        .WithAllFans(fullPower);

      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void NewFanSection_WithNoFans_ThrowsException()
    {
      var sectionBuilder = new FanSectionBuilder();

      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void TryingtoSpecifyAFanInAnUnsupportedDirection_ThrowsException()
    {
      var sectionBuilder = new FanSectionBuilder();
      Assert.Throws<InvalidOperationException>(() => sectionBuilder.WithFanInDirection(eDirection.South, fullPower));
    }

    [Test]
    public void NewFanSection_CanUpdateAllFansAtSametime()
    {
      var section = new FanSectionBuilder()
        .WithAllFans(halfPower)
        .Build();

      Assert.AreEqual(halfPower, section.East);
      Assert.AreEqual(halfPower, section.West);
    }

    [Test]
    public void FanSection_WithDifferentFanTypesOnEachFan_CorrectlySpecified()
    {
      var section = new FanSectionBuilder()
        .WithFanInDirection(eDirection.East, fullPower)
        .WithFanInDirection(eDirection.West, halfPower)
        .Build();

      Assert.AreEqual(fullPower, section.East);
      Assert.AreEqual(halfPower, section.West);
    }

    [Test]
    [TestCase(eDirection.East, eDirection.NorthEast)]
    [TestCase(eDirection.West, eDirection.NorthWest)]
    public void SpecifyingFanRepeatedly_InComplimentaryDirections_UsesTheLastAssignment(eDirection first, eDirection second)
    {
      var section = new FanSectionBuilder()
        .WithFanInDirection(first, fullPower)
        .WithFanInDirection(second, halfPower)
        .Build();

      Assert.AreEqual(halfPower, GetFanInDirection(section, second));
    }

    private Fan GetFanInDirection(FanSection fanSection, eDirection direction)
    {
      switch (direction)
      {
        case eDirection.East:
        case eDirection.NorthEast:
          return fanSection.East;
        case eDirection.West:
        case eDirection.NorthWest:
          return fanSection.West;
        default:
          throw new InvalidOperationException("Unexpected Direction");
      }
    }
  }
}