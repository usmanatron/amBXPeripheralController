using System;
using System.Reflection;
using NUnit.Framework;
using aPC.Client.Disco;
using System.Collections.Generic;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    [TestFixtureSetUp]
    public void SetupTestSettings()
    {
      mTestSettings = new Settings
      {
        //qqUMI finish
      };
    }

    [Test]
    public void NoGivenArguments_GivesDefaultSettings()
    {
      var lArgumentSettings = new ArgumentReader(new List<string>()).ParseArguments();
      var lDefaultSettings = new Settings();

      foreach (FieldInfo lField in typeof (Settings).GetFields(BindingFlags.Public | BindingFlags.Instance))
      {
        Assert.AreEqual(lField.GetValue(lDefaultSettings), lField.GetValue(lArgumentSettings));
      }
    }

    [Test]
    public void UnexpectedArgument_ThrowsException()
    {
      var lArguments = new List<string> { "TotallyNonExistantArgument:0,1" };
      var lArgumentReader = new ArgumentReader(lArguments);
      Assert.Throws<UsageException>(() => lArgumentReader.ParseArguments());
    }

    [Test]
    [TestCaseSource("GetArgumentCases")]
    public void SpecifyingAnOverridingArgument_CorrectlyAddedToSettings(SettingsTester xiSettingsTest)
    {
      var lArgs = new List<string> { xiSettingsTest.Argument };

      var lArgumentSettings = new ArgumentReader(lArgs).ParseArguments();

      Assert.AreEqual(xiSettingsTest.ExpectedValue, xiSettingsTest.RangeSelector(lArgumentSettings));
    }

    private readonly object[] GetArgumentCases = 
    {
      new SettingsTester(new Range(0.3f, 0.7f), "red:0.3,0.7", settings => settings.RedColourWidth),
      new SettingsTester(new Range(0.21f, 0.89f), "green:0.21,0.89", settings => settings.GreenColourWidth),
      new SettingsTester(new Range(0.1f, 0.55f), "blue:0.1,0.55", settings => settings.BlueColourWidth),
      new SettingsTester(new Range(0, 1), "lightintensity:0,1", settings => settings.LightIntensityWidth),
      new SettingsTester(new Range(0.4f, 0.9f), "fan:0.4,0.9", settings => settings.FanWidth),
      new SettingsTester(500, "bpm:120", settings => settings.PushInterval)
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
      var lArgumentReader = new ArgumentReader(lArgument);
      Assert.Throws<UsageException>(() => lArgumentReader.ParseArguments());
    }

    private Settings mTestSettings;
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
