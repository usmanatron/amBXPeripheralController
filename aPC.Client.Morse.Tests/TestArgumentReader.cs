using System.Collections.Generic;

namespace aPC.Client.Morse.Tests
{
  internal class TestArgumentReader : ArgumentReader
  {
    public TestArgumentReader(string xiArguments)
      : base(xiArguments)
    {
    }

    public string Message
    {
      get
      {
        return base.mMessage;
      }
    }

    public List<string> Switches
    {
      get
      {
        return base.mSwitches;
      }
    }
  }
}