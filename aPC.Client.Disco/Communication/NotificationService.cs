using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using aPC.Common.Entities;
using aPC.Common.Communication;
using System.ServiceModel;
using System.Xml.Serialization;
using System.IO;

namespace aPC.Client.Disco.Communication
{
  class NotificationService
  {


    public void Push(amBXScene xiScene)
    {
      //This client only supports pushing to localhost
      var lClient = new ChannelFactory<INotificationService>(
        new BasicHttpBinding(),
        CommunicationSettings.ServiceUrlTemplate
        .Replace(CommunicationSettings.HostnameHolder, "localhost"));

      var lScene = SerialiseScene(xiScene);

      var lOutput = lClient.CreateChannel().RunCustomScene(lScene);

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
  }
}
