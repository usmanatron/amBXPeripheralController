using System.Configuration;

namespace aPC.Client
{
  public class HostnameAccessor
  {
    public HostnameAccessor(HostnameInput xiHostnameInput)
    {
      mHostnameInput = xiHostnameInput;
    }

    public string Get()
    {
      return ConfigurationManager.AppSettings[HostnameKey];
    }

    public void Update()
    {
      var lNewHostname = GetNewHostname();
      ConfigurationManager.AppSettings[HostnameKey] = lNewHostname;
    }

    public string GetNewHostname()
    {
      mHostnameInput.ShowDialog();
      return mHostnameInput.NewHostname;
    }

    private const string HostnameKey = "hostname";
    private readonly HostnameInput mHostnameInput;
  }
}
