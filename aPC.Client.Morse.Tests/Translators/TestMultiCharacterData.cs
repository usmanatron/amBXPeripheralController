using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal class TestMultiCharacterData : TestCharacterBase
  {
    public readonly string Word;

    public TestMultiCharacterData(string word, List<IMorseBlock> expectedCode)
      : base(expectedCode)
    {
      Word = word;
    }

    public override string ToString()
    {
      return Word;
    }
  }
}