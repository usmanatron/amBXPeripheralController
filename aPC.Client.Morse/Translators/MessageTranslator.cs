using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Client.Morse.Codes;

namespace aPC.Client.Morse.Translators
{
  public class MessageTranslator
  {
    public MessageTranslator(string xiMessage)
    {
      mMessage = xiMessage;
    }


    public List<IMorseBlock> Translate()
    {
      return new List<IMorseBlock>();
    }




    private string mMessage;
  }
  // qqUMI Current Idea:
  /*
   * Break into words by space and Run TranslateWord ->
   * Break into character and run translate character -> 
   * Turns into character and adds spacers.  Speacers also added by TransC and TransW
   */
}
