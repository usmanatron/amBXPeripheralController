using aPC.Client.Morse.Codes;
using aPC.Client.Morse.Translators;
using System;
using System.Collections.Generic;

namespace aPC.Client.Morse.Tests.Translators
{
  internal class TestTranslator : TranslatorBase
  {
    public override IEnumerable<List<IMorseBlock>> TranslateContent(string content)
    {
      throw new NotImplementedException();
    }

    public override IMorseBlock Separator
    {
      get
      {
        throw new NotImplementedException();
      }
    }

    public new List<IMorseBlock> AddSeparatorsToList(IEnumerable<List<IMorseBlock>> list, IMorseBlock separator)
    {
      return base.AddSeparatorsToList(list, separator);
    }
  }
}