using NUnit.Framework;
using System.IO;
using aPC.Client.Console;

namespace aPC.Client.Tests.Console
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    [Test]
    [TestCase("One")]
    [TestCase("One|Two|Three")]
    public void NotHavingTwoArguments_ThrowsException(string xiArguments)
    {
      var lArguments = xiArguments.Split('|');
      Assert.Throws<UsageException>(() => GetSettingsFromArguments(lArguments));
    }

    [Test]
    [TestCase(@"blah")]
    // Incorrect slash
    [TestCase(@"\i")]
    [TestCase(@"\f")]
    public void UnexpectedFirstArgument_Throws(string xiFirstArgument)
    {
      var lArguments = new[] { xiFirstArgument, "OtherArg" };
      Assert.Throws<UsageException>(() => GetSettingsFromArguments(lArguments));
    }

    [Test]
    public void IntegratedScene_ParsedCorrectly()
    {
      var lArguments = new[] { @"/I", "SceneName" };
      var lSettings = GetSettingsFromArguments(lArguments);

      Assert.AreEqual(true, lSettings.IsIntegratedScene);
      Assert.AreEqual(lSettings.SceneData, lArguments[1]);
    }

    [Test]
    public void CustomScene_ParsedCorrectly()
    {
      var lArguments = new[] { @"/F", "ExampleScene.xml" };
      var lSettings = GetSettingsFromArguments(lArguments);

      Assert.AreEqual(false, lSettings.IsIntegratedScene);
      Assert.AreEqual(lSettings.SceneData, GetExampleScene(lArguments[1]));
    }

    [Test]
    public void CustomScene_WithIvalidPath_Throws()
    {
      var lArguments = new[] { @"/F", "DoesntExist.xml" };
      Assert.Throws<UsageException>(() => new ArgumentReader(lArguments));
    }

    #region Helpers

    private string GetExampleScene(string xiFilename)
    {
      return File.ReadAllText(Path.GetFullPath(xiFilename));
    }

    private Settings GetSettingsFromArguments(string[] xiArguments)
    {
      var lSettings = new Settings();
      new ArgumentReader(xiArguments).AddArgumentsToSettings(lSettings);
      return lSettings;
    }

    #endregion
  }
}
