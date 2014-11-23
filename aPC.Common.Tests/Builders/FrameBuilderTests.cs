using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System;
using System.Linq;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  internal class FrameBuilderTests
  {
    private static readonly LightSection lightSection = DefaultLightSections.JiraBlue;
    private static readonly FanSection fanSection = DefaultFanSections.Half;
    private static readonly RumbleSection rumbleSection = DefaultRumbleSections.Boing;

    [Test]
    public void SpecifyingFrameDataBeforeAddingAFrame_ThrowsException()
    {
      var builder = new FrameBuilder();
      Assert.Throws<NullReferenceException>(() => builder.WithFrameLength(100));
    }

    [Test]
    public void FrameLengthUnspecified_ThrowsException()
    {
      var builder = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithLightSection(lightSection)
        .WithFanSection(fanSection)
        .WithRumbleSection(rumbleSection);

      Assert.Throws<ArgumentException>(() => builder.Build());
    }

    [Test]
    public void IsRepeatedUnspecified_ThrowsException()
    {
      var builder = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(100)
        .WithLightSection(lightSection)
        .WithFanSection(fanSection)
        .WithRumbleSection(rumbleSection);

      Assert.Throws<ArgumentException>(() => builder.Build());
    }

    [Test]
    public void AllSectionsUnspecified_ThrowsException()
    {
      var builder = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(100)
        .WithRepeated(true);

      Assert.Throws<ArgumentException>(() => builder.Build());
    }

    [Test]
    [TestCaseSource("AddSectionCases")]
    public void RepeatedAndFrameLengthAndOneSectionSpecified_Builds(AddSection addSection)
    {
      var builder = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(100)
        .WithRepeated(true);

      addSection(builder);

      Assert.DoesNotThrow(() => builder.Build());
    }

    private readonly object[] AddSectionCases =
    {
      new AddSection(builder => builder.WithLightSection(lightSection)),
      new AddSection(builder => builder.WithFanSection(fanSection)),
      new AddSection(builder => builder.WithRumbleSection(rumbleSection))
    };

    public delegate FrameBuilder AddSection(FrameBuilder builder);

    [Test]
    public void NewlyBuiltFrame_HasExpectedData()
    {
      var frame = new FrameBuilder()
      .AddFrameWithDefaults()
      .Build()
      .Single();

      Assert.AreEqual(lightSection, frame.Lights);
      Assert.AreEqual(fanSection, frame.Fans);
      Assert.AreEqual(rumbleSection, frame.Rumbles);
    }

    [Test]
    public void MultipleFrames_AddedCorrectly()
    {
      var frames = new FrameBuilder()
        .AddFrameWithDefaults(200)
        .AddFrameWithDefaults(400)
        .Build();

      Assert.AreEqual(2, frames.Count);
      Assert.AreEqual(200, frames[0].Length);
      Assert.AreEqual(400, frames[1].Length);
    }
  }

  internal static class FrameBuilderExtensions
  {
    private static readonly LightSection lightSection = DefaultLightSections.JiraBlue;
    private static readonly FanSection fanSection = DefaultFanSections.Half;
    private static readonly RumbleSection rumbleSection = DefaultRumbleSections.Boing;

    public static FrameBuilder AddFrameWithDefaults(this FrameBuilder builder, int frameLength = 1000)
    {
      return builder
        .AddFrame()
        .WithFrameLength(frameLength)
        .WithRepeated(true)
        .WithLightSection(lightSection)
        .WithFanSection(fanSection)
        .WithRumbleSection(rumbleSection);
    }
  }
}