using System;
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
    


    /* Trying to manipulate a frame without calling AddFrame blows up
     * Need Repeated / FrameLength specified and one of Fans, Rumbles, Lights
     *  * Will be 4 tests?  
     *  WithLight actually adds a lightSection
     *  * similarly FansSection and RumbleSection
     *  
     */
    private static readonly LightSection mLightSection = DefaultLightSections.JiraBlue;
    private static readonly FanSection mFanSection = DefaultFanSections.Half;
    private static readonly RumbleSection mRumbleSection = DefaultRumbleSections.Boing;
  }
}
