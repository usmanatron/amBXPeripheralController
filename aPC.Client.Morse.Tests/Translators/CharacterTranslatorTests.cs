using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using NUnit.Framework;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  [TestFixture]
  internal class CharacterTranslatorTests
  {
    [Test]
    public void TranslateMessage_ReturnsListOfIMorseBlocks()
    {
      var lTranslatedMessage = new CharacterTranslator('A').Translate();
      Assert.AreEqual(typeof(List<IMorseBlock>), lTranslatedMessage.GetType());
    }

    [Test]
    [TestCaseSource("TestCharacters")]
    public void TranslatingOneCharacter_GivesExpectedMorseCode(TestSingleCharacterData xiData)
    {
      var lTranslatedCharacter = new CharacterTranslator(xiData.Character).Translate();

      Assert.AreEqual(xiData.ExpectedCodeCount, lTranslatedCharacter.Count);

      for (int i = 0; i < xiData.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(xiData.ExpectedCode[i].GetType(), lTranslatedCharacter[i].GetType());
      }
    }

    // The case doesn't matter
    private TestSingleCharacterData[] TestCharacters = new TestSingleCharacterData[]
    {
      new TestSingleCharacterData('A', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dash()}),
      new TestSingleCharacterData('C', new List<IMorseBlock> {new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dot()}),
      new TestSingleCharacterData('a', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dash()}),
      new TestSingleCharacterData('c', new List<IMorseBlock> {new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dot()}),
      new TestSingleCharacterData('?', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(),
                                                        new Dot(), new DotDashSeparator(), new Dot()}),
      new TestSingleCharacterData('_', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(),
                                                        new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(),
                                                        new Dot(), new DotDashSeparator(), new Dash()})
    };
  }
}