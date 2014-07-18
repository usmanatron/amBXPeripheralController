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
      //TODO: Get rid of this - right now it's necessary to stop an exception happening when hitting this
      // twice in the application lifetime.
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