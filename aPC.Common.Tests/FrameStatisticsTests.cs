using aPC.Common.Builders;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using NUnit.Framework;
using System.Linq;

namespace aPC.Common.Tests
{
  [TestFixture]
  internal class FrameStatisticsTests
  {
    private Light arbitraryLight;

    [TestFixtureSetUp]
    public void Setup()
    {
      arbitraryLight = DefaultLights.Red;
    }

    [Test]
    public void FrameStatistics_ContainsExpectedComponentType()
    {
      var lightSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithLightInDirection(eDirection.North, arbitraryLight)
        .Build();
      var frames = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(false)
        .WithLightSection(lightSection)
        .Build();
      var stats = new FrameStatistics(frames);

      Assert.IsTrue(stats.AreEnabledForComponent(eComponentType.Light));
      Assert.IsFalse(stats.AreEnabledForComponent(eComponentType.Fan));
      Assert.IsFalse(stats.AreEnabledForComponent(eComponentType.Rumble));

      Assert.IsTrue(stats.AreEnabledForComponentAndDirection(eComponentType.Light, eDirection.North));
      Assert.AreEqual(1, stats.EnabledDirectionalComponents.Count());
    }

    [Test]
    public void FrameStatistics_ContainsNoDuplicates()
    {
      var lightSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithAllLights(arbitraryLight)
        .Build();
      var frames = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(false)
        .WithLightSection(lightSection)
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(false)
        .WithLightSection(lightSection)
        .Build();
      var stats = new FrameStatistics(frames);

      Assert.AreEqual(8, stats.EnabledDirectionalComponents.Count);
    }

    [Test]
    public void FrameStatistics_RecordsSceneLength()
    {
      var lightSection = new LightSectionBuilder()
        .WithFadeTime(100)
        .WithAllLights(arbitraryLight)
        .Build();
      var frames = new FrameBuilder()
        .AddFrame()
        .WithFrameLength(1000)
        .WithRepeated(false)
        .WithLightSection(lightSection)
        .AddFrame()
        .WithFrameLength(500)
        .WithRepeated(false)
        .WithLightSection(lightSection)
        .Build();
      var stats = new FrameStatistics(frames);

      Assert.AreEqual(frames.Sum(frame => frame.Length), stats.SceneLength);
    }
  }
}