using aPC.Common.Defaults;
using NUnit.Framework;
using System.Linq;

namespace aPC.Client.Morse.Tests
{
  [TestFixture]
  internal class ArgumentReaderTests
  {
    [Test]
    [TestCase(null)]
    [TestCase("")]
    public void EmptyArguments_ThrowsUsageException(string xiArguments)
    {
      Assert.Throws<UsageException>(() => new TestArgumentReader(xiArguments));
    }

    [Test]
    public void ValidArguments_ReturnsSettings()
    {
      var lArgumentReader = new TestArgumentReader(@"/M:An example of valid arguments");
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(typeof(Settings), lSettings.GetType());
    }

    [Test]
    public void ArgumentString_CorrectlyBrokenIntoSeparateArguments()
    {
      var lArgumentString = @"/D /R /C:0,0,1 /U:250 /M:Message";
      var lArgumentReader = new TestArgumentReader(lArgumentString);

      var lSwitches = lArgumentReader.Switches;

      Assert.AreEqual(4, lSwitches.Count());
      Assert.AreEqual(@"/D", lSwitches[0]);
      Assert.AreEqual(@"/R", lSwitches[1]);
      Assert.AreEqual(@"/C:0,0,1", lSwitches[2]);
      Assert.AreEqual(@"/U:250", lSwitches[3]);
      Assert.AreEqual(@"Message", lArgumentReader.Message);
    }

    [Test]
    public void SpecifyingNoMessage_ThrowsException()
    {
      var lArgumentReader = new TestArgumentReader(@"/D /R");
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    public void DisablingLightsAndRumbles_ThrowsException()
    {
      var lArgumentReader = new TestArgumentReader(@"/L /M:A");
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    // All switches passed in are not case-sensitive
    [Test]
    [TestCase(@"/d /m:A")]
    [TestCase(@"/D /M:A")]
    public void Specifying_D_SetsRepeatable(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(true, lSettings.RepeatMessage);
    }

    [Test]
    [TestCase(@"/r /m:A")]
    [TestCase(@"/R /M:A")]
    public void Specifying_R_EnablesRumbles(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(true, lSettings.RumblesEnabled);
    }

    [Test]
    [TestCase(@"/l /r /m:A")]
    [TestCase(@"/L /R /M:A")]
    public void Specifying_L_DisablesLights(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(false, lSettings.LightsEnabled);
    }

    [Test]
    [TestCase(@"/u:200 /m:A")]
    [TestCase(@"/U:200 /M:A")]
    public void Specifying_U_SetsUnitLength(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(200, lSettings.UnitLength);
    }

    [Test]
    [TestCase(@"/U:blah /m:A")]
    public void Specifying_U_WithInvalidLength_Throws(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    [TestCase(@"/c:0,0.25,1 /m:A")]
    [TestCase(@"/C:0,0.25,1 /M:A")]
    public void Specifying_C_SetsColour(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
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
      var lArgumentReader = new TestArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    [TestCase(@"/C:0.1,0.3,bob /m:A")]
    public void Specifying_C_WithInvalidFloats_Throws(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    [TestCase(@"/C:0.1,0.3,1.2 /m:A")]
    [TestCase(@"/C:-0.1,0.3,1 /m:A")]
    public void Specifying_C_WithValuesOutOfRange_Throws(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    public void Specifying_C_WithAllValuesZero_Throws()
    {
      var lArgumentReader = new TestArgumentReader(@"/C:-0,0,0 /m:A");
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }

    [Test]
    public void SpecifyingOnlyMessage_DoesNotThrow()
    {
      var lArgumentReader = new TestArgumentReader(@"/M:Message");
      Assert.DoesNotThrow(() => lArgumentReader.Read());
    }

    [Test]
    public void SpecifyingOnlyMessage_PopulatesDefaultValues()
    {
      var lArgumentReader = new TestArgumentReader(@"/M:Message");
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual(true, lSettings.LightsEnabled);
      Assert.AreEqual(false, lSettings.RumblesEnabled);
      Assert.AreEqual(DefaultLights.White, lSettings.Colour);
      Assert.AreEqual(200, lSettings.UnitLength);
      Assert.AreEqual(false, lSettings.RepeatMessage);
    }

    [Test]
    public void SpecifyingMessage_PopulatesSettings()
    {
      var lArgumentReader = new TestArgumentReader(@"/M:Message");
      var lSettings = lArgumentReader.Read();

      Assert.AreEqual("Message", lSettings.Message);
    }

    /// <remarks>
    ///   See ArgumentReader for details on the list
    ///   of supported characters.
    /// </remarks>
    [Test]
    [TestCase(@"/M:£")]
    [TestCase(@"/M:Valid except for the last %")]
    [TestCase(@"/M:_ - _")]
    public void SpecifyingMessage_WithInvalidCharacters_Throws(string xiArg)
    {
      var lArgumentReader = new TestArgumentReader(xiArg);
      Assert.Throws<UsageException>(() => lArgumentReader.Read());
    }
  }
}