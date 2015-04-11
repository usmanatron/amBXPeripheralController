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
    private FanSectionBuilder sectionBuilder;

    [SetUp]
    public void Setup()
    {
      sectionBuilder = new FanSectionBuilder();
    }

    [Test]
    public void NewFanSection_WithNoFans_ThrowsException()
    {
      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void SettingAllFans_SetsBothFansWithSameValue()
    {
      var section = sectionBuilder
        .WithAllFans(halfPower)
        .Build();

      Assert.AreEqual(halfPower, section.GetComponentSectionInDirection(eDirection.East));
      Assert.AreEqual(halfPower, section.GetComponentSectionInDirection(eDirection.West));
    }

    [Test]
    public void FanSection_WithDifferentFanTypesOnEachFan_CorrectlySpecified()
    {
      var section = sectionBuilder
        .WithFanInDirection(eDirection.East, fullPower)
        .WithFanInDirection(eDirection.West, halfPower)
        .Build();

      Assert.AreEqual(fullPower, section.GetComponentSectionInDirection(eDirection.East));
      Assert.AreEqual(halfPower, section.GetComponentSectionInDirection(eDirection.West));
    }

    [Test]
    public void SpecifyingAFanOnTheSameDirectionTwice_Throws()
    {
      sectionBuilder.WithFanInDirection(eDirection.East, halfPower);

      Assert.Throws<ArgumentException>(() => sectionBuilder.WithFanInDirection(eDirection.East, halfPower));
    }
  }
}