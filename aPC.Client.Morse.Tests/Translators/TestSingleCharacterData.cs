using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal class TestSingleCharacterData : TestCharacterBase
  {
    public readonly char Character;

    public TestSingleCharacterData(char character, List<IMorseBlock> expectedCode)
      : base(expectedCode)
    {
      Character = character;
    }

    public override string ToString()
    {
      return Character.ToString();
    }
  }
}