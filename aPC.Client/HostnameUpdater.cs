using System.Configuration;

namespace aPC.Client
{
  class HostnameUpdater
  {
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
      var lWindow = new HostnameInput();
      lWindow.ShowDialog();
      return lWindow.NewHostname;
    }

    private const string HostnameKey = "hostname";
  }
}
