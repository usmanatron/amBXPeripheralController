using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Morse.Codes
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




    protected List<IMorseBlock> TranslateCharacter(char xiCharacter)
    {
      var lRawCharacter = Characters.RawCharacters[xiCharacter];
      

      return AddSeparatorsToList(lRawCharacter, typeof(DotDashSeparator));
    }


    /// <summary>
    ///   Adds an instance of the given type between every 
    ///   element in the list.  Does not add any new elements to the immediate start of end
    /// </summary>
    private List<IMorseBlock> AddSeparatorsToList(List<IMorseBlock> xiList, Type xiSeparatorType)
    {
      var lNewList = new List<IMorseBlock>();

      foreach (var lItem in xiList)
      {
        lNewList.Add(lItem);
        lNewList.Add((IMorseBlock) Activator.CreateInstance(xiSeparatorType));
      }

      lNewList.RemoveAt(lNewList.Count - 1);
      return lNewList;
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
