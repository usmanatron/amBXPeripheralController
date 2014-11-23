using aPC.Client.Morse.Codes;
using aPC.Common.Defaults;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Tests
{
  [TestFixture]
  internal class MorseFrameBuilderTests
  {
    [SetUp]
    public void TestSetup()
    {
      mSettings = new Settings("");
      mSettings.RumblesEnabled = true;
    }

    [Test]
    public void AddFramesFromMorseBlocks_AddsExpectedNumberOfFrames()
    {
      var lBlocks = new List<IMorseBlock> { new Dot(), new Dot() };
      var lFrames = new MorseFrameBuilder(mSettings)
        .AddFrames(lBlocks)
        .Build();

      Assert.AreEqual(2, lFrames.Count);
    }

    [Test]
    public void AddFramesFromMorseBlocks_AddsExpectedFrames()
    {
      var lBlocks = new List<IMorseBlock> { new Dot(), new Dash() };
      var lFrames = new MorseFrameBuilder(mSettings)
        .AddFrames(lBlocks)
        .Build();

      for (int i = 0; i < 2; i++)
      {
        var lColour = lBlocks[i].Enabled ? mSettings.Colour : DefaultLights.Off;
        var lRumble = lBlocks[i].Enabled ? mSettings.Rumble : DefaultRumbles.Off;

        Assert.IsNull(lFrames[i].Fans);
        Assert.IsNotNull(lFrames[i].Lights);
        Assert.IsNotNull(lFrames[i].Rumbles);

        Assert.AreEqual(lBlocks[i].Length * mSettings.UnitLength, lFrames[i].Length);
        Assert.AreEqual(lColour, lFrames[i].Lights.North);
        Assert.AreEqual(lRumble, lFrames[i].Rumbles.Rumble);
      }
    }

    [Test]
    [TestCaseSource("IMorseBlocks")]
    public void AddFrameFromMorseBlock_AddsExpectedFrame(IMorseBlock xiBlock)
    {
      var lFrames = new MorseFrameBuilder(mSettings)
        .AddFrames(new List<IMorseBlock>() { xiBlock })
        .Build();

      var lColour = xiBlock.Enabled ? mSettings.Colour : DefaultLights.Off;
      var lRumble = xiBlock.Enabled ? mSettings.Rumble : DefaultRumbles.Off;

      Assert.AreEqual(1, lFrames.Count);
      var lFrame = lFrames.Single();
      Assert.IsNull(lFrame.Fans);
      Assert.IsNotNull(lFrame.Lights);
      Assert.IsNotNull(lFrame.Rumbles);

      Assert.AreEqual(xiBlock.Length * mSettings.UnitLength, lFrame.Length);
      Assert.AreEqual(lColour, lFrame.Lights.North);
      Assert.AreEqual(lRumble, lFrame.Rumbles.Rumble);
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

    private Settings mSettings;
  }
}