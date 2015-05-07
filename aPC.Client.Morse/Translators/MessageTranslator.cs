using aPC.Client.Morse.Codes;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Translators
{
  public class MessageTranslator : TranslatorBase
  {
    private readonly WordTranslator baseTranslator;

    public MessageTranslator(WordTranslator wordTranslator)
    {
      baseTranslator = wordTranslator;
    }

    public override IEnumerable<List<IMorseBlock>> TranslateContent(string content)
    {
      return content.Split(' ')
        .Select(word => baseTranslator.Translate(word))
        .ToList();
    }

    public override IMorseBlock Separator
    {
      get
      {
        return new WordSeparator();
      }
    }
  }
}