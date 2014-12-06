using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public class MessageTranslator : TranslatorBase
  {
    private WordTranslator baseTranslator;

    public MessageTranslator(WordTranslator wordTranslator)
    {
      this.baseTranslator = wordTranslator;
    }

    public override IEnumerable<List<IMorseBlock>> TranslateContent(string content)
    {
      var translatedMessage = new List<List<IMorseBlock>>();

      foreach (var word in content.Split(' '))
      {
        translatedMessage.Add(baseTranslator.Translate(word));
      }

      return translatedMessage;
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