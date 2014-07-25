using System.Configuration;
using aPC.Common.Client;

namespace aPC.Client
{
  public class UpdatableHostnameAccessor : HostnameAccessor
  {
    public UpdatableHostnameAccessor(HostnameInput xiHostnameInput)
    {
      mHostnameInput = xiHostnameInput;
    }

    public override string Get()
    {
      return ConfigurationManager.AppSettings[HostnameKey];
    }

    public override void Update()
    {
      var lNewHostname = GetNewHostname();
      ConfigurationManager.AppSettings[HostnameKey] = lNewHostname;
    }

    private string GetNewHostname()
    {
      //TODO: Get rid of this - right now it's necessary to stop an exception happening when hitting this
      // twice in the applications lifetime.
      mHostnameInput = new HostnameInput();
      mHostnameInput.ShowDialog();
      var lNewHostname = mHostnameInput.NewHostname;

      return string.IsNullOrEmpty(lNewHostname)
        ? Get()
        : lNewHostname;
    }

    private const string HostnameKey = "hostname";
    private HostnameInput mHostnameInput;
  }
}