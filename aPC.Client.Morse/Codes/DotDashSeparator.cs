namespace aPC.Client.Morse.Codes
{
  class DotDashSeparator : IMorseBlock
  {
    public int Length
    {
      get
      {
        return 1;
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
