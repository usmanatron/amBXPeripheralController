using aPC.Client.Cli;
using aPC.Client.Shared;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;

namespace aPC.Client.Cli.Tests
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    [Test]
    [TestCase("One")]
    [TestCase("One|Two|Three")]
    public void NotHavingTwoArguments_ThrowsException(string arguments)
    {
      var splitArguments = arguments.Split('|');
      Assert.Throws<UsageException>(() => GetSettingsFromArguments(splitArguments));
    }

    [Test]
    [TestCase(@"blah")]
    // Incorrect slash
    [TestCase(@"\i")]
    [TestCase(@"\f")]
    public void UnexpectedFirstArgument_Throws(string firstArgument)
    {
      var arguments = new[] { firstArgument, "OtherArg" };
      Assert.Throws<UsageException>(() => GetSettingsFromArguments(arguments));
    }

    [Test]
    public void IntegratedScene_ParsedCorrectly()
    {
      var arguments = new[] { @"/I", "SceneName" };
      var settings = GetSettingsFromArguments(arguments);

      Assert.AreEqual(settings.SceneName, arguments[1]);
    }

    [Test]
    [Ignore]
    public void CustomScene_ParsedCorrectly()
    {
      var arguments = new[] { @"/F", "ExampleScene.xml" };
      var settings = GetSettingsFromArguments(arguments);

      //TODO: Fix once amBXScene equality works
      //Assert.AreEqual(settings.Scene, GetExampleScene(arguments[1]));
    }

    [Test]
    [Ignore]
    public void CustomScene_WithInvalidPath_Throws()
    {
      var arguments = new[] { @"/F", "DoesntExist.xml" };
      Assert.Throws<UsageException>(() => new ArgumentReader(new Settings(),arguments));
      //TODO: Fix once aPC.Client has been split
    }

    #region Helpers

    private string GetExampleScene(string filename)
    {
      return File.ReadAllText(Path.GetFullPath(filename));
    }

    private Settings GetSettingsFromArguments(IEnumerable<string> arguments)
    {
      var settings = new Settings();
      new ArgumentReader(settings, arguments).Read();
      return settings;
    }

    #endregion Helpers
  }
}