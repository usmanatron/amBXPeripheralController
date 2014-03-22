using aPC.Common.Client;

namespace aPC.Client.Morse
{
  public class UsageException : UsageExceptionBase
  {
    public UsageException(string xiDescription) : base(xiDescription)
    {
    }

    protected override string Usage()
    {
      throw new System.NotImplementedException();
    }
  }
}
