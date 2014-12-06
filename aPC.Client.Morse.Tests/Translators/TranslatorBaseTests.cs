using aPC.Client.Morse.Codes;
using NUnit.Framework;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  internal class TranslatorBaseTests
  {
    private TestTranslator translator;

    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      translator = new TestTranslator();
    }

    [Test]
    public void AddSeparators_AddsExpectedSeparators()
    {
      var list = new List<List<IMorseBlock>>() { new List<IMorseBlock> { new Dot() }, new List<IMorseBlock> { new Dot() } };

      var separatedList = translator.AddSeparatorsToList(list, new DotDashSeparator());

      Assert.AreEqual(3, separatedList.Count);
      Assert.AreEqual(list[0][0], separatedList[0]);
      Assert.AreEqual(list[1][0], separatedList[2]);
      Assert.AreEqual(typeof(DotDashSeparator), separatedList[1].GetType());
    }

    [Test]
    public void AddSeparators_WithSingleElementInList_DoesNothing()
    {
      var list = new List<List<IMorseBlock>>() { new List<IMorseBlock> { new Dot() } };

      var separatedList = translator.AddSeparatorsToList(list, new DotDashSeparator());

      Assert.AreEqual(1, separatedList.Count);
      Assert.AreEqual(list[0][0], separatedList[0]);
    }
  }
}