using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public interface ITranslator
  {
    IEnumerable<List<IMorseBlock>> TranslateContent(string content);

    IMorseBlock Separator { get; }
  }
}