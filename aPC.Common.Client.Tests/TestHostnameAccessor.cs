namespace aPC.Common.Client.Tests
{
  class TestHostnameAccessor : HostnameAccessorBase
  {
    public override string Get()
    {
      return "localhost";
    }
  }
}
