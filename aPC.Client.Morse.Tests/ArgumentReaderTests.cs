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
    public void EmptyArguments_ThrowsUsageException(string arguments)
    {
      Assert.Throws<UsageException>(() => new TestArgumentReader(arguments));
    }

    [Test]
    public void ValidArguments_ReturnsSettings()
    {
      var argumentReader = new TestArgumentReader(@"/M:An example of valid arguments");
      var settings = argumentReader.Read();

      Assert.AreEqual(typeof(Settings), settings.GetType());
    }

    [Test]
    public void ArgumentString_CorrectlyBrokenIntoSeparateArguments()
    {
      var argumentString = @"/D /R /C:0,0,1 /U:250 /M:Message";
      var argumentReader = new TestArgumentReader(argumentString);

      var switches = argumentReader.Switches;

      Assert.AreEqual(4, switches.Count());
      Assert.AreEqual(@"/D", switches[0]);
      Assert.AreEqual(@"/R", switches[1]);
      Assert.AreEqual(@"/C:0,0,1", switches[2]);
      Assert.AreEqual(@"/U:250", switches[3]);
      Assert.AreEqual(@"Message", argumentReader.Message);
    }

    [Test]
    public void SpecifyingNoMessage_ThrowsException()
    {
      var argumentReader = new TestArgumentReader(@"/D /R");
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }

    [Test]
    public void DisablingLightsAndRumbles_ThrowsException()
    {
      var argumentReader = new TestArgumentReader(@"/L /M:A");
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }

    // All switches passed in are not case-sensitive
    [Test]
    [TestCase(@"/d /m:A")]
    [TestCase(@"/D /M:A")]
    public void Specifying_D_SetsRepeatable(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      var settings = argumentReader.Read();

      Assert.AreEqual(true, settings.RepeatMessage);
    }

    [Test]
    [TestCase(@"/r /m:A")]
    [TestCase(@"/R /M:A")]
    public void Specifying_R_EnablesRumbles(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      var settings = argumentReader.Read();

      Assert.AreEqual(true, settings.RumblesEnabled);
    }

    [Test]
    [TestCase(@"/l /r /m:A")]
    [TestCase(@"/L /R /M:A")]
    public void Specifying_L_DisablesLights(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      var settings = argumentReader.Read();

      Assert.AreEqual(false, settings.LightsEnabled);
    }

    [Test]
    [TestCase(@"/u:200 /m:A")]
    [TestCase(@"/U:200 /M:A")]
    public void Specifying_U_SetsUnitLength(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      var settings = argumentReader.Read();

      Assert.AreEqual(200, settings.UnitLength);
    }

    [Test]
    [TestCase(@"/U:blah /m:A")]
    public void Specifying_U_WithInvalidLength_Throws(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }

    [Test]
    [TestCase(@"/c:0,0.25,1 /m:A")]
    [TestCase(@"/C:0,0.25,1 /M:A")]
    public void Specifying_C_SetsColour(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      var settings = argumentReader.Read();

      Assert.AreEqual(0, settings.Colour.Red);
      Assert.AreEqual(0.25f, settings.Colour.Green);
      Assert.AreEqual(1f, settings.Colour.Blue);
    }

    [Test]
    [TestCase(@"/C:0.1,0.3 /m:A")]
    [TestCase(@"/C:0.2,0.4,0.6,0.8 /M:A")]
    public void Specifying_C_WithIncorrectNumberOfValues_Throws(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }

    [Test]
    [TestCase(@"/C:0.1,0.3,bob /m:A")]
    public void Specifying_C_WithInvalidFloats_Throws(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }

    [Test]
    [TestCase(@"/C:0.1,0.3,1.2 /m:A")]
    [TestCase(@"/C:-0.1,0.3,1 /m:A")]
    public void Specifying_C_WithValuesOutOfRange_Throws(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }

    [Test]
    public void Specifying_C_WithAllValuesZero_Throws()
    {
      var argumentReader = new TestArgumentReader(@"/C:-0,0,0 /m:A");
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }

    [Test]
    public void SpecifyingOnlyMessage_DoesNotThrow()
    {
      var argumentReader = new TestArgumentReader(@"/M:Message");
      Assert.DoesNotThrow(() => argumentReader.Read());
    }

    [Test]
    public void SpecifyingOnlyMessage_PopulatesDefaultValues()
    {
      var argumentReader = new TestArgumentReader(@"/M:Message");
      var settings = argumentReader.Read();

      Assert.AreEqual(true, settings.LightsEnabled);
      Assert.AreEqual(false, settings.RumblesEnabled);
      Assert.AreEqual(DefaultLights.White, settings.Colour);
      Assert.AreEqual(200, settings.UnitLength);
      Assert.AreEqual(false, settings.RepeatMessage);
    }

    [Test]
    public void SpecifyingMessage_PopulatesSettings()
    {
      var argumentReader = new TestArgumentReader(@"/M:Message");
      var settings = argumentReader.Read();

      Assert.AreEqual("Message", settings.Message);
    }

    /// <remarks>
    ///   See ArgumentReader for details on the list
    ///   of supported characters.
    /// </remarks>
    [Test]
    [TestCase(@"/M:£")]
    [TestCase(@"/M:Valid except for the last %")]
    [TestCase(@"/M:_ - _")]
    public void SpecifyingMessage_WithInvalidCharacters_Throws(string arg)
    {
      var argumentReader = new TestArgumentReader(arg);
      Assert.Throws<UsageException>(() => argumentReader.Read());
    }
  }
}