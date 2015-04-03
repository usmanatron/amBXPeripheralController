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
    public void NewFanSection_WithNoFans_ThrowsException()
    {
      var sectionBuilder = new FanSectionBuilder();

      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void NewFanSection_CanUpdateAllFansAtSametime()
    {
      var section = new FanSectionBuilder()
        .WithAllFans(halfPower)
        .Build();

      Assert.AreEqual(halfPower, section.GetComponentValueInDirection(eDirection.East));
      Assert.AreEqual(halfPower, section.GetComponentValueInDirection(eDirection.West));
    }

    [Test]
    public void FanSection_WithDifferentFanTypesOnEachFan_CorrectlySpecified()
    {
      var section = new FanSectionBuilder()
        .WithFanInDirection(eDirection.East, fullPower)
        .WithFanInDirection(eDirection.West, halfPower)
        .Build();

      Assert.AreEqual(fullPower, section.GetComponentValueInDirection(eDirection.East));
      Assert.AreEqual(halfPower, section.GetComponentValueInDirection(eDirection.West));
    }
  }
}