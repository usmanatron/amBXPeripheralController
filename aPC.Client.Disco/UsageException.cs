using aPC.Common.Client;
using System;

namespace aPC.Client.Disco
{
  [Serializable]
  public class UsageException : UsageExceptionBase
  {
    public UsageException(string xiDescription)
      : base(xiDescription)
    {
    }

    protected override string Usage()
    {
      return 
@"Usage: Disco [Arguments]

All arguments of the form A:B where A is the name and B is the value.
The following are supported:

BPM : Beats per minute

red, blue, green, fan

qqUMI

";
    }
  }
}
