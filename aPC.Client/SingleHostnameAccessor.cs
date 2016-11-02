using System.Configuration;
using System.Linq;
using aPC.Common.Client;

namespace aPC.Client
{
  public class SingleHostnameAccessor
  {
    private const string HostnameKey = "hostname";
    private readonly HostnameAccessor hostnameAccessor;

    public SingleHostnameAccessor(HostnameAccessor hostnameAccessor)
    {
      this.hostnameAccessor = hostnameAccessor;
      hostnameAccessor.ResetWith(ConfigurationManager.AppSettings[HostnameKey]);
    }

    public void PersistConfig(string hostname)
    {
      ConfigurationManager.AppSettings[HostnameKey] = hostname;
    }

    public string Get()
    {
      return hostnameAccessor.GetAll().Single();
    }

    public void Update(string newHostname)
    {
      hostnameAccessor.ResetWith(newHostname);
    }
  }
}