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
      var word = "Boom!";

      var translatedMessage = new MessageTranslator(word).Translate();
      var translatedWord = new WordTranslator(word).Translate();

      Assert.AreEqual(translatedWord.Count(), translatedMessage.Count());
      for (var i = 0; i < translatedWord.Count(); i++)
      {
        Assert.AreEqual(translatedWord[i].GetType(), translatedMessage[i].GetType());
      }
    }

    [Test]
    public void TwoWords_ReturnsWordsWithOneSeparator()
    {
      var firstWord = "Tick";
      var secondWord = "Tock";

      var translatedMessage = new MessageTranslator(firstWord + " " + secondWord).Translate();

      var firstTranslatedWord = new WordTranslator(firstWord).Translate();
      var secondTranslatedWord = new WordTranslator(secondWord).Translate();
      var expectedMessage = firstTranslatedWord;
      expectedMessage.Add(new WordSeparator());
      expectedMessage.AddRange(secondTranslatedWord);

      Assert.AreEqual(expectedMessage.Count(), translatedMessage.Count());
      for (var i = 0; i < expectedMessage.Count(); i++)
      {
        Assert.AreEqual(expectedMessage[i].GetType(), translatedMessage[i].GetType());
      }
    }

    [Test]
    [TestCaseSource("TestMessages")]
    public void SomeExampleWords_ReturnExpectedMorseCode(TestMultiCharacterData data)
    {
      var translatedWord = new MessageTranslator(data.Word).Translate();

      Assert.AreEqual(data.ExpectedCodeCount, translatedWord.Count);

      for (int i = 0; i < data.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(data.ExpectedCode[i].GetType(), translatedWord[i].GetType());
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