using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using aPC.Client.Morse.Codes;

namespace aPC.Client.Morse.Tests.Codes
{
  [TestFixture]
  class MessageTranslatorTests
  {
    [Test]
    [TestCaseSource("TestCharacters")]
    public void TranslatingOneCharacter_GivesExpectedMorseCode(TestCharacterData xiData)
    {
      var lTranslatedCharacter = new TestMessageTranslator("").TranslateCharacter(xiData.Character);

      Assert.AreEqual(xiData.ExpectedCodeCount, lTranslatedCharacter.Count);

      for (int i = 0; i < xiData.ExpectedCodeCount; i++)
      {
        Assert.AreEqual(xiData.ExpectedCode[i].GetType(), lTranslatedCharacter[i].GetType());
      }
    }

    private TestCharacterData[] TestCharacters = new TestCharacterData[]
    {
      new TestCharacterData('A', new List<IMorseBlock> {new Dot(), new DotDashSeparator(), new Dash()}),
      new TestCharacterData('C', new List<IMorseBlock> {new Dash(), new DotDashSeparator(), new Dot(), new DotDashSeparator(), 
                                                        new Dash(), new DotDashSeparator(), new Dot()}),
    };

    [Test]
    public void TranslateMessage_ReturnsListOfIMorseBlocks()
    {
      var lTranslatedMessage = new TestMessageTranslator("Message").Translate();

      Assert.AreEqual(typeof(List<IMorseBlock>), lTranslatedMessage.GetType());
    }
  }
}
