using System;

namespace aPC.Common.Client
{
  [Serializable]
  public abstract class UsageExceptionBase : Exception
  {
    public UsageExceptionBase(string xiDescription)
    {
      mUserDescription = xiDescription;
    }

    public void DisplayUsage()
    {
      Console.WriteLine("Error: " + mUserDescription);
      Console.WriteLine(Environment.NewLine + Environment.NewLine);
      Console.WriteLine(Usage());
    }

    protected abstract string Usage();

    private readonly string mUserDescription;
  }
}
