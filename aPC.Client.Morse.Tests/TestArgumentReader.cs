using System.Collections.Generic;

namespace aPC.Client.Morse.Tests
{
  internal class TestArgumentReader : ArgumentReader
  {
    public TestArgumentReader(string arguments)
      : base(arguments)
    {
    }

    public string Message
    {
      get
      {
        return base.message;
      }
    }

    public List<string> Switches
    {
      get
      {
        return base.switches;
      }
    }
  }
}