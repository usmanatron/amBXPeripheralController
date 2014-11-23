namespace aPC.Client.Morse.Codes
{
  /// <summary>
  ///   Marks when we've hit the end of the message.
  /// </summary>
  /// <remarks>
  ///   This is strictly *against* the Morse Code specification, however
  ///   the actual "end of message" prosign is fairly complicated (eight dots).
  ///   Therefore, it feels like this would be more obvious.
  /// </remarks>
  public class MessageEndMarker : IMorseBlock
  {
    public int Length
    {
      get
      {
        return 12;
      }
    }

    public bool Enabled
    {
      get
      {
        return false;
      }
    }
  }
}