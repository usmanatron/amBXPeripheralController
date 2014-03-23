using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Codes
{
  class TestMessageTranslator : MessageTranslator
  {
    public TestMessageTranslator(string xiMessage) : base (xiMessage)
    {
    }

    public new List<IMorseBlock> TranslateCharacter(char xiCharacter)
    {
      return base.TranslateCharacter(xiCharacter);
    }
  }
}
