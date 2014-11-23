using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  internal class MessageTranslatorTests
  {
    [Test]
    public void SingleWordMessage_ReturnsTheWord()
    {
      var lWord = "Boom!";

      var lTranslatedMessage = new MessageTranslator(lWord).Translate();
      var lTranslatedWord = new WordTranslator(lWord).Translate();

      Assert.AreEqual(lTranslatedWord.Count(), lTranslatedMessage.Count());
      for (var i = 0; i < lTranslatedWord.Count(); i++)
      {
        Assert.AreEqual(lTranslatedWord[i].GetType(), lTranslatedMessage[i].GetType());
      }
    }

    [Test]
    public void TwoWords_ReturnsWordsWithOneSeparator()
    {
      var lFirstWord = "Tick";
      var lSecondWord = "Tock";

      var lTranslatedMessage = new MessageTranslator(lFirstWord.ToString() + " " + lSecondWord.ToString()).Translate();

      var lFirstTranslatedWord = new WordTranslator(lFirstWord).Translate();
      var lSecondTranslatedWord = new WordTranslator(lSecondWord).Translate();
      var lExpectedMessage = lFirstTranslatedWord;
      lExpectedMessage.Add(new WordSeparator());
      lExpectedMessage.AddRange(lSecondTranslatedWord);

      Assert.AreEqual(lExpectedMessage.Count(), lTranslatedMessage.Count());
      for (var i = 0; i < lExpectedMessage.Count(); i++)
      {
        Assert.AreEqual(lExpectedMessage[i].GetType(), lTranslatedMessage[i].GetType());
      }
    }

    [Test]
    [TestCaseSource("TestMessages")]
    public void SomeExampleWords_ReturnExpectedMorseCode(TestMultiCharacterData xiData)
    {
      var lTranslatedWord = new MessageTranslator(xiData.Word).Translate();

      Assert.AreEqual(xiData.ExpectedCodeCount, lTranslatedWord.Count);

      for (int i = 0; i < xiData.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(xiData.ExpectedCode[i].GetType(), lTranslatedWord[i].GetType());
      }
    }

    private TestMultiCharacterData[] TestMessages = new TestMultiCharacterData[]
    {
      new TestMultiCharacterData("1 2 3", new List<IMorseBlock>
      {
        new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash(),  new DotDashSeparator(),new Dash(), new DotDashSeparator(), new Dash(),
        new WordSeparator(),
        new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash(),
        new WordSeparator(),
        new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash()
      }),
      new TestMultiCharacterData("Its A-B", new List<IMorseBlock>
      {
        new Dot(), new DotDashSeparator(), new Dot(), new CharacterSeparator(),
        new Dash(), new CharacterSeparator(),
        new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(),
        new WordSeparator(),
        new Dot(), new DotDashSeparator(), new Dash(), new CharacterSeparator(),
        new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dash(), new CharacterSeparator(),
        new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot()
      })
    };
  }
}