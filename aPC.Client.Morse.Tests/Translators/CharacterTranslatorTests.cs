using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  internal class CharacterTranslatorTests
  {
    private CharacterTranslator translator;

    [SetUp]
    public void Setup()
    {
      translator = new CharacterTranslator();
    }

    [Test]
    public void TranslateMessage_ReturnsListOfIMorseBlocks()
    {
      var translatedMessage = translator.Translate("A");
      Assert.AreEqual(typeof(List<IMorseBlock>), translatedMessage.GetType());
    }

    [Test]
    [TestCaseSource("TestCharacters")]
    public void TranslatingOneCharacter_GivesExpectedMorseCode(TestSingleCharacterData data)
    {
      var translatedCharacter = translator.Translate(data.Content);

      Assert.AreEqual(data.ExpectedCodeCount, translatedCharacter.Count);

      for (int i = 0; i < data.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(data.ExpectedCode[i].GetType(), translatedCharacter[i].GetType());
      }
    }

    [Test]
    public void AttemptingToTranslateMultipleCharacters_Throws()
    {
      Assert.Throws<InvalidOperationException>(() => translator.Translate("ABC"));
    }

    // The case doesn't matter
    private TestSingleCharacterData[] TestCharacters = new TestSingleCharacterData[]
    {
      new TestSingleCharacterData("A", new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dash()}),
      new TestSingleCharacterData("C", new List<IMorseBlock> {new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dot()}),
      new TestSingleCharacterData("a", new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dash()}),
      new TestSingleCharacterData("c", new List<IMorseBlock> {new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dot()}),
      new TestSingleCharacterData("?", new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(),
                                                        new Dot(), new DotDashSeparator(), new Dot()}),
      new TestSingleCharacterData("_", new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(),
                                                        new Dot(), new DotDashSeparator(), new Dash()})
    };
  }
}