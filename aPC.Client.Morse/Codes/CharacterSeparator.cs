namespace aPC.Client.Morse.Codes
{
  class CharacterSeparator : IMorseBlock
  {
    public int Length
    {
      get
      {
        return 3;
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
