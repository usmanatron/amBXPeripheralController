using System;
using aPC.Common.Entities;
using aPC.Common.Communication;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;

namespace aPC.Client.Disco.Communication
{
  class NotificationService
  {
    public NotificationService()
    {
      //This client only supports pushing to localhost
      mClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        CommunicationSettings.ServiceUrlTemplate
        .Replace(CommunicationSettings.HostnameHolder, "localhost"));
    }

    public void Push(amBXScene xiScene)
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
