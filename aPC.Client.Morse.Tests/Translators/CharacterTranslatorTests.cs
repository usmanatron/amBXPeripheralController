using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;

namespace aPC.Client.Morse.Tests.Codes
{
  [TestFixture]
  class CharacterTranslatorTests
  {
    [Test]
    public void TranslateMessage_ReturnsListOfIMorseBlocks()
    {
      var lTranslatedMessage = new CharacterTranslator('A').Translate();
      Assert.AreEqual(typeof(List<IMorseBlock>), lTranslatedMessage.GetType());
    }

    [Test]
    [TestCaseSource("TestCharacters")]
    public void TranslatingOneCharacter_GivesExpectedMorseCode(TestCharacterData xiData)
    {
      var lTranslatedCharacter = new CharacterTranslator(xiData.Character).Translate();

      Assert.AreEqual(xiData.ExpectedCodeCount, lTranslatedCharacter.Count);

      for (int i = 0; i < xiData.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(xiData.ExpectedCode[i].GetType(), lTranslatedCharacter[i].GetType());
      }
    }

    // The case doesn't matter
    private TestCharacterData[] TestCharacters = new TestCharacterData[]
    {
      new TestCharacterData('A', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dash()}),
      new TestCharacterData('C', new List<IMorseBlock> {new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), 
                                                        new Dash(), new DotDashSeparator(), new Dot()}),
      new TestCharacterData('a', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dash()}),
      new TestCharacterData('c', new List<IMorseBlock> {new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), 
                                                        new Dash(), new DotDashSeparator(), new Dot()}),
      new TestCharacterData('?', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), 
                                                        new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), 
                                                        new Dot(), new DotDashSeparator(), new Dot()}),
      new TestCharacterData('_', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), 
                                                        new Dash(), new DotDashSeparator(), new Dash(), new DotDashSeparator(), 
                                                        new Dot(), new DotDashSeparator(), new Dash()})
    };
  }
}
