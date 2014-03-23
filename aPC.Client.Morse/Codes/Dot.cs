namespace aPC.Client.Morse.Codes
{
  public class Dot : IMorseBlock
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
        return true;
      }
    }
  }
}
