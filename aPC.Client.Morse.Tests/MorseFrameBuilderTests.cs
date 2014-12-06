using aPC.Client.Morse.Codes;
using aPC.Common.Builders;
using aPC.Common.Defaults;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Tests
{
  [TestFixture]
  internal class MorseFrameBuilderTests
  {
    private Settings settings;
    private MorseFrameBuilder frameBuilder;

    [SetUp]
    public void Setup()
    {
      settings = new Settings("");
      settings.RumblesEnabled = true;
      frameBuilder = new MorseFrameBuilder(new LightSectionBuilder(), new RumbleSectionBuilder());
    }

    [Test]
    public void AddFramesFromMorseBlocks_AddsExpectedNumberOfFrames()
    {
      var blocks = new List<IMorseBlock> { new Dot(), new Dot() };
      var frames = frameBuilder.AddFrames(settings, blocks)
        .Build();

      Assert.AreEqual(2, frames.Count);
    }

    [Test]
    public void AddFramesFromMorseBlocks_AddsExpectedFrames()
    {
      var blocks = new List<IMorseBlock> { new Dot(), new Dash() };
      var frames = frameBuilder.AddFrames(settings, blocks)
        .Build();

      for (int i = 0; i < 2; i++)
      {
        var colour = blocks[i].Enabled ? settings.Colour : DefaultLights.Off;
        var rumble = blocks[i].Enabled ? settings.Rumble : DefaultRumbles.Off;

        Assert.IsNull(frames[i].Fans);
        Assert.IsNotNull(frames[i].Lights);
        Assert.IsNotNull(frames[i].Rumbles);

        Assert.AreEqual(blocks[i].Length * settings.UnitLength, frames[i].Length);
        Assert.AreEqual(colour, frames[i].Lights.North);
        Assert.AreEqual(rumble, frames[i].Rumbles.Rumble);
      }
    }

    [Test]
    [TestCaseSource("IMorseBlocks")]
    public void AddFrameFromMorseBlock_AddsExpectedFrame(IMorseBlock block)
    {
      var frames = frameBuilder.AddFrames(settings, new List<IMorseBlock>() { block })
        .Build();

      var colour = block.Enabled ? settings.Colour : DefaultLights.Off;
      var rumble = block.Enabled ? settings.Rumble : DefaultRumbles.Off;

      Assert.AreEqual(1, frames.Count);
      var frame = frames.Single();
      Assert.IsNull(frame.Fans);
      Assert.IsNotNull(frame.Lights);
      Assert.IsNotNull(frame.Rumbles);

      Assert.AreEqual(block.Length * settings.UnitLength, frame.Length);
      Assert.AreEqual(colour, frame.Lights.North);
      Assert.AreEqual(rumble, frame.Rumbles.Rumble);
    }

    private IEnumerable<IMorseBlock> IMorseBlocks
    {
      get
      {
        yield return new Dot();
        yield return new Dash();
        yield return new CharacterSeparator();
        yield return new DotDashSeparator();
        yield return new WordSeparator();
        yield return new MessageEndMarker();
      }
    }
  }
}