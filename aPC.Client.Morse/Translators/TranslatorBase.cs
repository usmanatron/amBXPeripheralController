using aPC.Client.Morse.Codes;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Morse.Translators
{
  public abstract class TranslatorBase : ITranslator
  {
    public List<IMorseBlock> Translate(string content)
    {
      ThrowIfInputInvalid(content);

      var translatedMessage = TranslateContent(content);

      return AddSeparatorsToList(translatedMessage, Separator);
    }

    // Default implementation assumes it's valid
    protected virtual void ThrowIfInputInvalid(string content)
    {
    }

    public abstract IEnumerable<List<IMorseBlock>> TranslateContent(string content);

    public abstract IMorseBlock Separator { get; }

    /// <summary>
    ///   Adds an instance of the given type between every list of elements
    ///   in the list.  Does not add any new elements to the immediate start of end.
    /// </summary>
    protected List<IMorseBlock> AddSeparatorsToList(IEnumerable<List<IMorseBlock>> list, IMorseBlock separator)
    {
      var newList = new List<IMorseBlock>();

      foreach (var item in list)
      {
        newList.AddRange(item);
        newList.Add(separator);
      }

      newList.RemoveAt(newList.Count - 1);
      return newList;
    }
  }
}