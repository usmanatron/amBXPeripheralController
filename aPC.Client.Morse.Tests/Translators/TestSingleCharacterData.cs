using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal class TestSingleCharacterData : TestCharacterBase
  {
    public readonly string Content;

    public TestSingleCharacterData(string content, List<IMorseBlock> expectedCode)
      : base(expectedCode)
    {
      Content = content;
    }
  }
}