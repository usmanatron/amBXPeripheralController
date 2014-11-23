using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal class TestSingleCharacterData : TestCharacterBase
  {
    public TestSingleCharacterData(char xiCharacter, List<IMorseBlock> xiExpectedCode)
      : base(xiExpectedCode)
    {
      Character = xiCharacter;
    }

    public override string ToString()
    {
      return Character.ToString();
    }

    public readonly char Character;
  }
}