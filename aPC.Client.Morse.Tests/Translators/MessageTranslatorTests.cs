using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using FakeItEasy;
using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  internal class MessageTranslatorTests
  {
    private MessageTranslator translator;
    private WordTranslator wordTranslator;

    private static readonly IMorseBlock dot = new Dot();
    private static readonly IMorseBlock dash = new Dash();
    private static readonly IMorseBlock dotDashSeparator = new DotDashSeparator();
    private static readonly IMorseBlock characterSeparator = new CharacterSeparator();
    private static readonly IMorseBlock wordSeparator = new WordSeparator();

    [SetUp]
    public void Setup()
    {
      wordTranslator = new WordTranslator(new CharacterTranslator());
      translator = new MessageTranslator(wordTranslator);
    }

    [Test]
    [TestCaseSource("TestMessages")]
    public void SomeExampleWords_ReturnExpectedMorseCode(TestMultiCharacterData data)
    {
      var translatedWord = translator.Translate(data.Word);

      Assert.AreEqual(data.ExpectedCodeCount, translatedWord.Count);

      for (int i = 0; i < data.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(data.ExpectedCode[i].GetType(), translatedWord[i].GetType());
      }
    }

    private readonly TestMultiCharacterData[] TestMessages = new TestMultiCharacterData[]
    {
      new TestMultiCharacterData("Boom", new List<IMorseBlock> {
        dash, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot, characterSeparator,
        dash, dotDashSeparator, dash, dotDashSeparator, dash, characterSeparator,
        dash, dotDashSeparator, dash, dotDashSeparator, dash, characterSeparator,
        dash, dotDashSeparator, dash
      }),
      new TestMultiCharacterData("Tick Tock", new List<IMorseBlock> {
        dash, characterSeparator,
        dot, dotDashSeparator, dot, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dash, dotDashSeparator, dot, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dash,
        wordSeparator,
        dash, characterSeparator,
        dash, dotDashSeparator, dash, dotDashSeparator, dash, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dash, dotDashSeparator, dot, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dash
      }),
      new TestMultiCharacterData("1 2 3", new List<IMorseBlock>
      {
        dot, dotDashSeparator, dash, dotDashSeparator, dash,  dotDashSeparator,dash, dotDashSeparator, dash,
        wordSeparator,
        dot, dotDashSeparator, dot, dotDashSeparator, dash, dotDashSeparator, dash, dotDashSeparator, dash,
        wordSeparator,
        dot, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dash, dotDashSeparator, dash
      }),
      new TestMultiCharacterData("Its A-B", new List<IMorseBlock>
      {
        dot, dotDashSeparator, dot, characterSeparator,
        dash, characterSeparator,
        dot, dotDashSeparator, dot, dotDashSeparator, dot,
        wordSeparator,
        dot, dotDashSeparator, dash, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dash, characterSeparator,
        dash, dotDashSeparator, dot, dotDashSeparator, dot, dotDashSeparator, dot
      })
    };
  }
}