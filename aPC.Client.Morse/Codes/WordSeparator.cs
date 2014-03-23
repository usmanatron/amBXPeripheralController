namespace aPC.Client.Morse.Codes
{
  class WordSeparator : IMorseBlock
  {
    public int Length
    {
      get
      {
        return 7;
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
