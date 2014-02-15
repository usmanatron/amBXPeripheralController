using System;

namespace aPC.Client.Disco
{
  //qqUMI - This is a carbon copy of the one in aPC.Client (except for the usage statement).  Ideally should commonise!
  [Serializable]
  public class UsageException : Exception
  {
    public UsageException(string xiDescription)
    {
      mUserDescription = xiDescription;
    }

    public void DisplayUsage()
    {
      Console.WriteLine("Error: " + mUserDescription);
      Console.WriteLine(Environment.NewLine + Environment.NewLine);
      Console.WriteLine(mUsage);
    }

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
