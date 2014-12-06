using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using FakeItEasy;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  internal class WordTranslatorTests
  {
    private WordTranslator translator;
    private CharacterTranslator baseTranslator;

    private static IMorseBlock dot = new Dot();
    private static IMorseBlock dash = new Dash();
    private static IMorseBlock dotDashSeparator = new DotDashSeparator();
    private static IMorseBlock characterSeparator = new CharacterSeparator();

    [SetUp]
    public void Setup()
    {
      baseTranslator = new CharacterTranslator();
      translator = new WordTranslator(baseTranslator);
    }

    [Test]
    public void InputWord_CannotContainSpaces()
    {
      Assert.Throws<InvalidOperationException>(() => translator.Translate("Space Space"));
    }

    [Test]
    [TestCaseSource("TestWords")]
    public void SomeExampleWords_ReturnExpectedMorseCode(TestMultiCharacterData data)
    {
      var translatedWord = translator.Translate(data.Word);

      Assert.AreEqual(data.ExpectedCodeCount, translatedWord.Count);

      for (int i = 0; i < data.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(data.ExpectedCode[i].GetType(), translatedWord[i].GetType());
      }
    }

    private TestMultiCharacterData[] TestWords = new TestMultiCharacterData[]
    {
      new TestMultiCharacterData("A", new List<IMorseBlock>
      {
        dot, dotDashSeparator, dash
      }),
      new TestMultiCharacterData("AD", new List<IMorseBlock>
      {
        dot, dotDashSeparator, dash, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dot
      }),
      new TestMultiCharacterData("CAR6", new List<IMorseBlock>
      {
        dash, dotDashSeparator, dot, dotDashSeparator, dash, dotDashSeparator, dot, characterSeparator,
        dot, dotDashSeparator, dash, characterSeparator,
        dot, dotDashSeparator, dash, dotDashSeparator, dot, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot
      }),
      new TestMultiCharacterData("BLOB", new List<IMorseBlock>
      {
        dash, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot, characterSeparator,
        dot, dotDashSeparator, dash, dotDashSeparator, dot, dotDashSeparator, dot, characterSeparator,
        dash, dotDashSeparator, dash, dotDashSeparator, dash, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot
      }),
      new TestMultiCharacterData("WHY?", new List<IMorseBlock>
      {
        dot, dotDashSeparator, dash, dotDashSeparator, dash, characterSeparator,
        dot, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dash, dotDashSeparator, dash, characterSeparator,
        dot, dotDashSeparator, dot, dotDashSeparator, dash, dotDashSeparator, dash, dotDashSeparator, dot, dotDashSeparator, dot
      })
    };
  }
}