using aPC.Client.Console;
using NUnit.Framework;
using System.IO;

namespace aPC.Client.Tests.Console
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

      Assert.AreEqual(true, settings.IsIntegratedScene);
      Assert.AreEqual(settings.SceneData, arguments[1]);
    }

    [Test]
    public void CustomScene_ParsedCorrectly()
    {
      var arguments = new[] { @"/F", "ExampleScene.xml" };
      var settings = GetSettingsFromArguments(arguments);

      Assert.AreEqual(false, settings.IsIntegratedScene);
      Assert.AreEqual(settings.SceneData, GetExampleScene(arguments[1]));
    }

    [Test]
    public void CustomScene_WithIvalidPath_Throws()
    {
      var arguments = new[] { @"/F", "DoesntExist.xml" };
      Assert.Throws<UsageException>(() => new ArgumentReader(arguments));
    }

    #region Helpers

    private string GetExampleScene(string filename)
    {
      return File.ReadAllText(Path.GetFullPath(filename));
    }

    private Settings GetSettingsFromArguments(string[] arguments)
    {
      var settings = new Settings();
      new ArgumentReader(arguments).AddArgumentsToSettings(settings);
      return settings;
    }

    #endregion Helpers
  }
}