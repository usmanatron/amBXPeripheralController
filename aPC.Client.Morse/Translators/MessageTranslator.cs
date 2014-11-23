using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public class MessageTranslator : TranslatorBase
  {
    private string message;

    public MessageTranslator(string message)
    {
      this.message = message;
    }

    public override List<IMorseBlock> Translate()
    {
      var translatedMessage = new List<List<IMorseBlock>>();

      foreach (var word in message.Split(' '))
      {
        translatedMessage.Add(new WordTranslator(word).Translate());
      }

      return AddSeparatorsToList(translatedMessage, new WordSeparator());
    }
  }
}