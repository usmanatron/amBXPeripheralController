using aPC.Client.Morse.Codes;
using System.Collections.Generic;

namespace aPC.Client.Morse.Translators
{
  public class MessageTranslator : TranslatorBase
  {
    public MessageTranslator(string xiMessage)
    {
      mMessage = xiMessage;
    }

    public override List<IMorseBlock> Translate()
    {
      var lTranslatedMessage = new List<List<IMorseBlock>>();

      foreach (var lWord in mMessage.Split(' '))
      {
        lTranslatedMessage.Add(new WordTranslator(lWord).Translate());
      }

      return AddSeparatorsToList(lTranslatedMessage, new WordSeparator());
    }

    private string mMessage;
  }
}