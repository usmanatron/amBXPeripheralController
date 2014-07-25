using aPC.Common.Client;

namespace aPC.Client.Disco
{
  class HostnameAccessor : HostnameAccessorBase
  {
    public override string Get()
    {
      return "localhost";
    }
  }
}
