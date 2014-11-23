using System.Configuration;

namespace aPC.Client
{
  //TODO: This should use the common HostnameAccessor instead of re-inventing it here!
  public class UpdatableHostnameAccessor
  {
    private const string HostnameKey = "hostname";
    private HostnameInput hostnameInput;

    public UpdatableHostnameAccessor(HostnameInput hostnameInput)
    {
      this.hostnameInput = hostnameInput;
    }

    public string Get()
    {
      return ConfigurationManager.AppSettings[HostnameKey];
    }

    public void Update()
    {
      var newHostname = GetNewHostname();
      ConfigurationManager.AppSettings[HostnameKey] = newHostname;
    }

    private string GetNewHostname()
    {
      //TODO: Use ninject somehow here - right now it's necessary to stop an exception happening when hitting this
      // twice in the applications lifetime.
      hostnameInput = new HostnameInput();
      hostnameInput.ShowDialog();
      var newHostname = hostnameInput.NewHostname;

      return string.IsNullOrEmpty(newHostname)
        ? Get()
        : newHostname;
    }
  }
}