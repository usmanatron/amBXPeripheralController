using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Client.Morse;
using NUnit.Framework;
using aPC.Common.Defaults;

namespace aPC.Client.Morse
{
  [TestFixture]
  class ArgumentReaderTests
  {
    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void EmptyArguments_ThrowsUsageException(string xiArguments)
    {
      Assert.Throws<UsageException>(() => new ArgumentReader(xiArguments));
    }

    [Test]
    public void ValidArguments_ReturnsSettings()
    {
      var lArgumentReader = new ArgumentReader(@"/M:Valid Arguments");
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(typeof(Settings), lSettings.GetType());
    }

    [Test]
    public void ArgumentString_CorrectlyBrokenIntoSeparateArguments()
    {
      var lArgumentString = @"/D /R /C:0,0,1 /M:Message";
      var lArgumentReader = new ArgumentReader(lArgumentString);

      var lSwitches = lArgumentReader.Switches;

      Assert.AreEqual(3, lSwitches.Count());
      Assert.AreEqual(@"/D", lSwitches[0]);
      Assert.AreEqual(@"/R", lSwitches[1]);
      Assert.AreEqual(@"/C:0,0,1", lSwitches[2]);
      Assert.AreEqual(@"Message", lArgumentReader.Message);
    }

    [Test]
    public void SpecifyingNoMessage_ThrowsException()
    {
      var lArgumentReader = new ArgumentReader(@"/D /R");
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    // All switches passed in are not case-sensitive
    [Test]
    [TestCase(@"/d /m:A")]
    [TestCase(@"/D /M:A")]
    public void Specifying_D_SetsRepeatable(string xiArg)
    {
      var lArgumentReader = new ArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(true, lSettings.RepeatMessage);
    }

    [Test]
    [TestCase(@"/r /m:A")]
    [TestCase(@"/R /M:A")]
    public void Specifying_R_EnablesRumbles(string xiArg)
    {
      var lArgumentReader = new ArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(true, lSettings.RumblesEnabled);
    }

    [Test]
    [TestCase(@"/-l /m:A")]
    [TestCase(@"/-L /M:A")]
    public void Specifying_L_DisablesLights(string xiArg)
    {
      var lArgumentReader = new ArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(false, lSettings.LightsEnabled);
    }

    [Test]
    [TestCase(@"/c:0,0.25,1 /m:A")]
    [TestCase(@"/C:0,0.25,1 /M:A")]
    public void Specifying_C_SetsColour(string xiArg)
    {
      var lArgumentReader = new ArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(0, lSettings.Colour.Red);
      Assert.AreEqual(0.25f, lSettings.Colour.Green);
      Assert.AreEqual(1f, lSettings.Colour.Blue);
    }

    [Test]
    [TestCase(@"/C:0.1,0.3 /m:A")]
    [TestCase(@"/C:0.2,0.4,0.6,0.8 /M:A")]
    public void Specifying_C_WithIncorrectNumberOfValues_Throws(string xiArg)
    {
      var lArgumentReader = new ArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    [TestCase(@"/C:0.1,0.3,bob /m:A")]
    public void Specifying_C_WithInvalidFloats_Throws(string xiArg)
    {
      var lArgumentReader = new ArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    [TestCase(@"/C:0.1,0.3,1.2 /m:A")]
    [TestCase(@"/C:-0.1,0.3,1 /m:A")]
    public void Specifying_C_WithValuesOutOfRange_Throws(string xiArg)
    {
      var lArgumentReader = new ArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    public void SpecufyingOnlyMessage_DoesNotThrow()
    {
      var lArgumentReader = new ArgumentReader(@"/M:Message");
      Assert.DoesNotThrow(() => lArgumentReader.Read());
    }

    [Test]
    public void SpecufyingOnlyMessage_PopulatesDefaultValues()
    {
      var lArgumentReader = new ArgumentReader(@"/M:Message");
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(true, lSettings.LightsEnabled);
      Assert.AreEqual(false, lSettings.RumblesEnabled);
      Assert.AreEqual(DefaultLights.White, lSettings.Colour);
      Assert.AreEqual(false, lSettings.RepeatMessage);
    }
  }
}
