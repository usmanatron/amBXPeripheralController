using aPC.Common.Communication;
using System.ServiceModel;

namespace aPC.Common.Client.Communication
{
  internal class ClientConnection
  {
    public readonly string Hostname;
    public readonly ChannelFactory<INotificationService> Client;

    public ClientConnection(string xiHostname, ChannelFactory<INotificationService> client)
    {
      Hostname = xiHostname;
      Client = client;
    }
  }
}