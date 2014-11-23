using aPC.Common.Communication;
using System.ServiceModel;

namespace aPC.Common.Client.Communication
{
  internal class ClientConnection
  {
    public ClientConnection(string xiHostname, ChannelFactory<INotificationService> xiClient)
    {
      Hostname = xiHostname;
      Client = xiClient;
    }

    public readonly string Hostname;
    public readonly ChannelFactory<INotificationService> Client;
  }
}