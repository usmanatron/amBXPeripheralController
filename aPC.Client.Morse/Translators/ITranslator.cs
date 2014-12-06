using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public interface ITranslator
  {
    List<IMorseBlock> Translate(string content);

    IMorseBlock Separator { get; }
  }
}