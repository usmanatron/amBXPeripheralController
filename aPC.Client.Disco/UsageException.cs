using aPC.Common.Client;
using System;

namespace aPC.Client.Disco
{
  [Serializable]
  public class UsageException : UsageExceptionBase
  {
    public UsageException(string description)
      : base(description)
    {
    }

    protected override string Usage()
    {
      return
@"Usage: Disco [Arguments]
All arguments of the form A:B where A is the name and B is the value.
The following are supported:

BPM       : Beats per minute.  Expects an integer value.  Default is 150
            Example - BPM:300

Red \     : The range of colour to use when building random scenes.  Expects
Blue \      two values between 0 and 1 (a min and max).  The default for
Green \     all colours is a range between 0 and 1 (i.e. the full colour
            spectrum).
            Example - red:0.5,1 means that all lights generated will have
            a red component of at least 0.5 and at most 1

Servers   : (Optional) A comma-separated list of servers to push Disco
            scenes to.  Each server must be running it's own copy of
            aPC.Server. If servers if ommitted, the default is localhost only.

Fan   : A fan range (deprecated - do not use!)
";
    }
  }
}