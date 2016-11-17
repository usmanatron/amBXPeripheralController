using aPC.Common.Communication;
using System.ServiceModel;

namespace aPC.Common.Client.Communication
{
  internal class HostConnection
  {
    public readonly string Hostname;
    public readonly ChannelFactory<INotificationService> HostService;

    public HostConnection(string xiHostname, ChannelFactory<INotificationService> host)
    {
      Hostname = xiHostname;
      HostService = host;
    }
  }
}