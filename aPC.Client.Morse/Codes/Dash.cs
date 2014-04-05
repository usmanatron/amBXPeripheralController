namespace aPC.Client.Morse.Codes
{
  public class Dash : IMorseBlock
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
        return true;
      }
    }
  }
}
