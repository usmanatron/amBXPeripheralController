using System;
using System.Linq;
using NUnit.Framework;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;

namespace aPC.Common.Tests.Builders
{
  [TestFixture]
  class FrameBuilderTests
  {
    [Test]
    public void SpecifyingFrameDataBeforeAddingAFrame_ThrowsException()
    {
      var lBuilder = new FrameBuilder();
      Assert.Throws<NullReferenceException>(() => lBuilder.WithFrameLength(100));
    }

    [Test]
    public void FrameLengthUnspecified_ThrowsException() 
    {
      var lBuilder = new FrameBuilder()
        .AddFrame()
        .WithRepeated(true)
        .WithLightSection(mLightSection)
        .WithFanSection(mFanSection)
        .WithRumbleSection(mRumbleSection);

      Assert.Throws<ArgumentException>(() => lBuilder.Build());
    }

    [Test]
    public void IsRepeatedUnspecified_ThrowsException()
    {
      var lBuilder = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(100)
        .WithLightSection(mLightSection)
        .WithFanSection(mFanSection)
        .WithRumbleSection(mRumbleSection);

      Assert.Throws<ArgumentException>(() => lBuilder.Build());
    }

    [Test]
    public void AllSectionsUnspecified_ThrowsException()
    {
      var lBuilder = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(100)
        .WithRepeated(true);

      Assert.Throws<ArgumentException>(() => lBuilder.Build());
    }

    [Test]
    [TestCaseSource("AddSectionCases")]
    public void RepeatedAndFrameLengthAndOneSectionSpecified_Builds(AddSection xiAddSection)
    {
      var lBuilder = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(100)
        .WithRepeated(true);

      xiAddSection(lBuilder);

      Assert.DoesNotThrow(() => lBuilder.Build());
    }

    private readonly object[] AddSectionCases = 
    {
      new AddSection(builder => builder.WithLightSection(mLightSection)),
      new AddSection(builder => builder.WithFanSection(mFanSection)),
      new AddSection(builder => builder.WithRumbleSection(mRumbleSection))
    };

    public delegate FrameBuilder AddSection(FrameBuilder xiBuilder);
    
    [Test]
    public void NewlyBuiltFrame_HasExpectedData()
    {
      var lFrame = new FrameBuilder()
      .AddFrameWithDefaults()
      .Build()
      .Single();

      Assert.AreEqual(mLightSection, lFrame.Lights);
      Assert.AreEqual(mFanSection, lFrame.Fans);
      Assert.AreEqual(mRumbleSection, lFrame.Rumbles);
    }

    [Test]
    public void MultipleFrames_AddedCorrectly()
    {
      var lFrames = new FrameBuilder()
        .AddFrameWithDefaults(200)
        .AddFrameWithDefaults(400)
        .Build();

      Assert.AreEqual(2, lFrames.Count);
      Assert.AreEqual(200, lFrames[0].Length);
      Assert.AreEqual(400, lFrames[1].Length);
    }
    
    private static readonly LightSection mLightSection = DefaultLightSections.JiraBlue;
    private static readonly FanSection mFanSection = DefaultFanSections.Half;
    private static readonly RumbleSection mRumbleSection = DefaultRumbleSections.Boing;
  }

  static class FrameBuilderExtensions
  {
    public static FrameBuilder AddFrameWithDefaults(this FrameBuilder xiBuilder, int xiFrameLength = 1000)
    {
      return xiBuilder
        .AddFrame()
        .WithFrameLength(xiFrameLength)
        .WithRepeated(true)
        .WithLightSection(mLightSection)
        .WithFanSection(mFanSection)
        .WithRumbleSection(mRumbleSection);
    }

    private static readonly LightSection mLightSection = DefaultLightSections.JiraBlue;
    private static readonly FanSection mFanSection = DefaultFanSections.Half;
    private static readonly RumbleSection mRumbleSection = DefaultRumbleSections.Boing;
  }
}
