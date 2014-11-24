using aPC.Common.Client;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using GetSetting = aPC.Client.Disco.Tests.ArgumentReaderTests.GetSetting;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    public delegate object GetSetting(Settings xiSettings);

    [Test]
    public void NoGivenArguments_GivesDefaultSettings()
    {
      var reader = new ArgumentReader(new List<string>(), new Settings(new HostnameAccessor()));
      var argumentSettings = reader.ParseArguments();
      var defaultSettings = GetDefaultSettings();

      foreach (FieldInfo field in typeof(Settings).GetFields(BindingFlags.Public | BindingFlags.Instance))
      {
        if (field.FieldType == typeof(HostnameAccessor))
        {
          var defaultAccessor = (HostnameAccessor)field.GetValue(defaultSettings);
          var actualAccessor = (HostnameAccessor)field.GetValue(defaultSettings);
          Assert.AreEqual(defaultAccessor.GetAll().Single(), actualAccessor.GetAll().Single());
        }
        else
        {
          Assert.AreEqual(field.GetValue(defaultSettings), field.GetValue(argumentSettings));
        }
      }
    }

    private Settings GetDefaultSettings()
    {
      return new Settings(new HostnameAccessor());
    }

    [Test]
    public void UnexpectedArgument_ThrowsException()
    {
      var arguments = new List<string> { "TotallyNonExistantArgument:0,1" };
      var argumentReader = new ArgumentReader(arguments, GetDefaultSettings());
      Assert.Throws<UsageException>(() => argumentReader.ParseArguments());
    }

    [Test]
    [TestCaseSource("GetArgumentCases")]
    public void SpecifyingAnOverridingArgument_CorrectlyAddedToSettings(SettingsTester settingsTest)
    {
      var args = new List<string> { settingsTest.Argument };

      var argumentSettings = new ArgumentReader(args, GetDefaultSettings()).ParseArguments();

      Assert.AreEqual(settingsTest.ExpectedValue, settingsTest.RangeSelector(argumentSettings));
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
      var argument = new List<string> { xiRange };
      var argumentReader = new ArgumentReader(argument, GetDefaultSettings());
      Assert.Throws<UsageException>(() => argumentReader.ParseArguments());
    }
  }

  public class SettingsTester
  {
    public object ExpectedValue;
    public string Argument;
    public GetSetting RangeSelector;

    public SettingsTester(object expectedValue, string argument, GetSetting rangeSelector)
    {
      ExpectedValue = expectedValue;
      Argument = argument;
      RangeSelector = rangeSelector;
    }
  }
}