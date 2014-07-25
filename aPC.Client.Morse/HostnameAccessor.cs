using aPC.Common.Client;

namespace aPC.Client.Morse
{
  class HostnameAccessor : HostnameAccessorBase
  {
    public override string Get()
    {
      return "localhost";
    }
  }
}
