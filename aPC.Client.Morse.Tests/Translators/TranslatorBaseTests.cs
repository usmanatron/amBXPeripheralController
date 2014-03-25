using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Client.Morse.Codes;
using NUnit.Framework;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  class TranslatorBaseTests
  {
    [TestFixtureSetUp]
    public void FixtureSetup()
    {
      mTranslator = new TestTranslator();
    }

    [Test]
    public void AddSeparators_AddsExpectedSeparators()
    {
      var lList = new List<IMorseBlock>() { new Dot(), new Dot() };

      var lSeparatedList = mTranslator.SeparateList(lList, new DotDashSeparator());

      Assert.AreEqual(3, lSeparatedList.Count);
      Assert.AreEqual(lList[0], lSeparatedList[0]);
      Assert.AreEqual(lList[1], lSeparatedList[2]);
      Assert.AreEqual(typeof(DotDashSeparator), lSeparatedList[1].GetType());
    }

    [Test]
    public void AddSeparators_WithSingleElementInList_DoesNothing()
    {
      var lList = new List<IMorseBlock>() { new Dot() };

      var lSeparatedList = mTranslator.SeparateList(lList, new DotDashSeparator());

      Assert.AreEqual(1, lSeparatedList.Count);
      Assert.AreEqual(lList[0], lSeparatedList[0]);
    }

    private TestTranslator mTranslator;
  }
}
