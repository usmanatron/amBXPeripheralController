using aPC.Common.Communication;
using System;
using System.Linq;
using System.ServiceModel;

namespace aPC.Common.Client.Communication
{
  class ClientConnection
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
