using System;
using NUnit.Framework;
using aPC.Client.Disco;
using System.Collections.Generic;

namespace aPC.Client.Disco.Tests
{
  [TestFixture]
  public class ArgumentReaderTests
  {
    [Test]
    public void NoGivenArguments_GivesDefaultSettings()
    {
      var lArgumentSettings = new ArgumentReader(new List<string>()).ParseArguments();
      var lDefaultSettings = new Settings();


      Assert.AreEqual(lDefaultSettings.BlueColourWidth, lArgumentSettings.BlueColourWidth);
      Assert.AreEqual(lDefaultSettings.PushInterval, lArgumentSettings.PushInterval);

    }
  }
}
