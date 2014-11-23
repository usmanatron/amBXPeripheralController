using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  internal class WordTranslatorTests
  {
    [Test]
    public void InputWord_CannotContainSpaces()
    {
      Assert.Throws<InvalidOperationException>(() => new WordTranslator("Space Space"));
    }

    [Test]
    public void SingleCharacterWord_ReturnsTheCharacter()
    {
      var character = 'A';

      var translatedWord = new WordTranslator(character.ToString()).Translate();
      var translatedCharacter = new CharacterTranslator(character).Translate();

      Assert.AreEqual(translatedCharacter.Count(), translatedWord.Count());
      for (var i = 0; i < translatedCharacter.Count(); i++)
      {
        Assert.AreEqual(translatedCharacter[i].GetType(), translatedWord[i].GetType());
      }
    }

    [Test]
    public void TwoCharacters_ReturnsCharactersWithOneSeparator()
    {
      var firstCharacter = '-';
      var secondCharacter = '?';

      var translatedWord = new WordTranslator(firstCharacter.ToString() + secondCharacter.ToString()).Translate();

      var firstTranslatedCharacter = new CharacterTranslator(firstCharacter).Translate();
      var secondTranslatedCharacter = new CharacterTranslator(secondCharacter).Translate();
      var expectedWord = firstTranslatedCharacter;
      expectedWord.Add(new CharacterSeparator());
      expectedWord.AddRange(secondTranslatedCharacter);

      Assert.AreEqual(expectedWord.Count(), translatedWord.Count());
      for (var i = 0; i < expectedWord.Count(); i++)
      {
        Assert.AreEqual(expectedWord[i].GetType(), translatedWord[i].GetType());
      }
    }

    [Test]
    [TestCaseSource("TestWords")]
    public void SomeExampleWords_ReturnExpectedMorseCode(TestMultiCharacterData data)
    {
      var translatedWord = new WordTranslator(data.Word).Translate();

      Assert.AreEqual(data.ExpectedCodeCount, translatedWord.Count);

      for (int i = 0; i < data.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(data.ExpectedCode[i].GetType(), translatedWord[i].GetType());
      }
    }

    private TestMultiCharacterData[] TestWords = new TestMultiCharacterData[]
    {
      new TestMultiCharacterData("CAR6", new List<IMorseBlock>
      {
        new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dot(), new CharacterSeparator(),
        new Dot(), new DotDashSeparator(), new Dash(), new CharacterSeparator(),
        new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dot(), new CharacterSeparator(),
        new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot()
      }),
      new TestMultiCharacterData("BLOB", new List<IMorseBlock>
      {
        new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new CharacterSeparator(),
        new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new CharacterSeparator(),
        new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash(), new CharacterSeparator(),
        new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot()
      }),
      new TestMultiCharacterData("WHY?", new List<IMorseBlock>
      {
        new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash(), new CharacterSeparator(),
        new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot(), new CharacterSeparator(),
        new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash(), new CharacterSeparator(),
        new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), new Dot()
      })
    };
  }
}