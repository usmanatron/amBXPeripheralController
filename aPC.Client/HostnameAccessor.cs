using System.Configuration;

namespace aPC.Client
{
  //qqUMI This needs to use the HostnameAccessor instead of re-inventing it here!  This also probably won't work...
  public class UpdatableHostnameAccessor
  {
    public UpdatableHostnameAccessor(HostnameInput xiHostnameInput)
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

    private string GetNewHostname()
    {
      //TODO: Use ninject somehow here - right now it's necessary to stop an exception happening when hitting this
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