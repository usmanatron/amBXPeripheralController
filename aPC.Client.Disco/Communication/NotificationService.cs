using System;
using aPC.Common.Communication;
using System.ServiceModel;
using aPC.Client.Disco;
using aPC.Common;

namespace aPC.Client.Disco.Communication
{
  class NotificationClient : INotificationClient
  {
    public NotificationClient()
    {
      //This client only supports pushing to localhost
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        CommunicationSettings.ServiceUrlTemplate
        .Replace(CommunicationSettings.HostnameHolder, "localhost"));
    }

    public void PushCustomScene(string xiScene)
    {
      var lOutput = mClient.CreateChannel().RunCustomScene(xiScene);

      if (!string.IsNullOrEmpty(lOutput))
      {
        Console.WriteLine("An error occurred when communicating to the server:");
        Console.WriteLine(Environment.NewLine);
        Console.WriteLine(lOutput);
      }
    }

    public void PushIntegratedScene(string xiScene)
    {
      throw new NotImplementedException("The disco task does not use integrated scenes");
    }

    private ChannelFactory<INotificationService> mClient;
  }
}
