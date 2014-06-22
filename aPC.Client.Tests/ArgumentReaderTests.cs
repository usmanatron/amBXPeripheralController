using System.Linq;
using NUnit.Framework;
using System.Collections.Generic;
using System.IO;
using aPC.Client;

namespace aPC.Client.Tests
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

    private string GetExampleScene(string xiFilename)
    {
      return File.ReadAllText(Path.GetFullPath(xiFilename));
    }

    private Settings GetSettingsFromArguments(string[] xiArguments)
    {
      return new ArgumentReader(xiArguments.ToList()).ParseArguments();
    }
  }
}
