using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;

namespace aPC.Common.Tests.Builders
{
  internal class RumbleSectionBuilderTests
  {
    private readonly Rumble arbitraryRumble = DefaultRumbles.Thunder;
    private RumbleSectionBuilder sectionBuilder;

    [SetUp]
    public void Setup()
    {
      sectionBuilder = new RumbleSectionBuilder();
    }

    [Test]
    public void NewRumbleSection_WithoutRumbles_Throws()
    {
      Assert.Throws<ArgumentException>(() => sectionBuilder.Build());
    }

    [Test]
    public void SettingACentralRumble_BuildsTheExpectedRumbleSection()
    {
      var section = sectionBuilder
        .WithRumbleInDirection(eDirection.Center, arbitraryRumble)
        .Build();

      Assert.AreEqual(arbitraryRumble, section.GetComponentSectionInDirection(eDirection.Center));
    }

    [Test]
    public void SpecifyingARumbleOnTheSameDirectionTwice_Throws()
    {
      sectionBuilder.WithRumbleInDirection(eDirection.Center, arbitraryRumble);

      Assert.Throws<ArgumentException>(() => sectionBuilder.WithRumbleInDirection(eDirection.Center, arbitraryRumble));
    }
  }
}