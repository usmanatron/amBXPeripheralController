using System;
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

      Assert.AreEqual(lDefaultSettings.BlueColourWidth, lArgumentSettings.BlueColourWidth);
      Assert.AreEqual(lDefaultSettings.PushInterval, lArgumentSettings.PushInterval);
      //qqUMI finish
    }

    [Test]
    public void SpecifyingAnyRangeNotBetween0and1_ThrowsException()
    {
      var lArguments = new List<string> { "red:0,3" };
      Assert.Throws<UsageException>(() => new ArgumentReader(lArguments).ParseArguments());
    }

    //qqUMI more test cases
    [Test]
    public void SpecifyingACustomRange_CorrectlyAddedToSettings()
    {
      var lArguments = new List<string> { "red:0.3,0.7" };
      var lArgumentSettings = new ArgumentReader(lArguments).ParseArguments();

      Assert.AreEqual(new Range(0.3f, 0.7f), lArgumentSettings.RedColourWidth);
    }

    private Settings mTestSettings;
  }
}
