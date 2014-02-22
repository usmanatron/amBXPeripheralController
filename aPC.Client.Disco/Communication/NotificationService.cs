using System;
using aPC.Common.Entities;
using aPC.Common.Communication;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;
using aPC.Client.Disco;
using aPC.Common;

namespace aPC.Client.Disco.Communication
{
  class NotificationService : INotificationClient
  {
    public NotificationService()
    {
      //This client only supports pushing to localhost
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        CommunicationSettings.ServiceUrlTemplate
        .Replace(CommunicationSettings.HostnameHolder, "localhost"));
    }

    public void PushCustomScene(amBXScene xiScene)
    {
      var lScene = SerialiseScene(xiScene);
      var lOutput = mClient.CreateChannel().RunCustomScene(lScene);

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

    private string SerialiseScene(amBXScene xiScene)
    {
      using (var lWriter = new StringWriter())
      {
        var lSerializer = new XmlSerializer(typeof(amBXScene));
        lSerializer.Serialize(lWriter, xiScene);
        return lWriter.ToString();
      }
    }

    private ChannelFactory<INotificationService> mClient;
  }
}
