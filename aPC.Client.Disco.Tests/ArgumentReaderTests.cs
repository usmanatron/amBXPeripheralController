using aPC.Common.Client;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    // TODO: This test fails because of HostnameAccessor.  One potential fix is to override Equals etc...
    [Test]
    public void NoGivenArguments_GivesDefaultSettings()
    {
      var lReader = new ArgumentReader(new List<string>(), new Settings(new HostnameAccessor()));
      var lArgumentSettings = lReader.ParseArguments();
      var lDefaultSettings = GetDefaultSettings();

      foreach (FieldInfo lField in typeof(Settings).GetFields(BindingFlags.Public | BindingFlags.Instance))
      {
        Assert.AreEqual(lField.GetValue(lDefaultSettings), lField.GetValue(lArgumentSettings));
      }
    }

    private Settings GetDefaultSettings()
    {
      return new Settings(new HostnameAccessor());
    }

    [Test]
    public void UnexpectedArgument_ThrowsException()
    {
      var lArguments = new List<string> { "TotallyNonExistantArgument:0,1" };
      var lArgumentReader = new ArgumentReader(lArguments, GetDefaultSettings());
      Assert.Throws<UsageException>(() => lArgumentReader.ParseArguments());
    }

    [Test]
    [TestCaseSource("GetArgumentCases")]
    public void SpecifyingAnOverridingArgument_CorrectlyAddedToSettings(SettingsTester xiSettingsTest)
    {
      var lArgs = new List<string> { xiSettingsTest.Argument };

      var lArgumentSettings = new ArgumentReader(lArgs, GetDefaultSettings()).ParseArguments();

      Assert.AreEqual(xiSettingsTest.ExpectedValue, xiSettingsTest.RangeSelector(lArgumentSettings));
    }

    private readonly object[] GetArgumentCases =
    {
      new SettingsTester(new Range(0.3f, 0.7f), "red:0.3,0.7", settings => settings.RedColourWidth),
      new SettingsTester(new Range(0.21f, 0.89f), "green:0.21,0.89", settings => settings.GreenColourWidth),
      new SettingsTester(new Range(0.1f, 0.55f), "blue:0.1,0.55", settings => settings.BlueColourWidth),
      new SettingsTester(new Range(0, 1), "intensity:0,1", settings => settings.LightIntensityWidth),
      new SettingsTester(200, "bpm:300", settings => settings.PushInterval),
      new SettingsTester("KRAKEN", "servers:KRAKEN", settings => settings.HostnameAccessor.GetAll().Single()),
      new SettingsTester(new List<string> { "ONE", "TWO" }, "servers:ONE,TWO", settings => settings.HostnameAccessor.GetAll())
    };

    [Test]
    // Invalid number of arguments
    [TestCase("red:0.1,0.5,0.9")]
    [TestCase("red:0.7")]
    // Arguments out of range
    [TestCase("red:0.5,2")]
    [TestCase("red:-1,0.2")]
    [TestCase("red:2,4")]
    public void SpecifyingACustomRange_WithInvalidData_ThrowsException(string xiRange)
    {
      var lArgument = new List<string> { xiRange };
      var lArgumentReader = new ArgumentReader(lArgument, GetDefaultSettings());
      Assert.Throws<UsageException>(() => lArgumentReader.ParseArguments());
    }
  }

  public class SettingsTester
  {
    public SettingsTester(object xiExpectedValue, string xiArgument, GetSetting xiRangeSelector)
    {
      ExpectedValue = xiExpectedValue;
      Argument = xiArgument;
      RangeSelector = xiRangeSelector;
    }

    public object ExpectedValue;
    public string Argument;
    public GetSetting RangeSelector;
  }

  public delegate object GetSetting(Settings xiSettings);
}