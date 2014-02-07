using System;

namespace aPC.Client.Disco
{
  //qqUMI - This is a carbon copy of the one in aPC.Client (except for the usage statement).  Ideally should commonise!
  [Serializable]
  public class UsageException : Exception
  {
    public UsageException(Exception xiException, string xiDescription)
    {
      mException = xiException;
      mUserDescription = xiDescription;
    }

    public UsageException(string xiDescription)
    {
      mUserDescription = xiDescription;
    }

    public void DisplayUsage()
    {
      Console.WriteLine("Error: " + mUserDescription);
      Console.WriteLine(Environment.NewLine + Environment.NewLine);

      if (mException != null)
      {
        Console.WriteLine("Underlying Exception: " +
                          Environment.NewLine + mException.Message +
                          Environment.NewLine + mException.StackTrace);
      }
      Console.WriteLine(mUsage);
    }

    private readonly Exception mException;
    private readonly string mUserDescription;

    private const string mUsage =
@"Usage: Disco [Arguments]

All arguments of the form A:B where A is the name and B is the value.
The following are supported:

BPM : Beats per minute

red, blue, green, fan

qqUMI

";
  }
}
