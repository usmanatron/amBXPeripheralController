using System.IO;
using System.Messaging;
using System.Xml.Serialization;
using Common.Entities;

namespace Server.Communication
{
  class MessageParser
  {
    public amBXScene GetNewScene()
    {
      try
      {
        var lMessage = MSMQAdministrator.Read();
        return DeserialiseScene(GetMessageContents(lMessage));
      }
      catch
      {
        // failed to read => We assume it hasn't changed
        return null;
      }
    }

    private string GetMessageContents(Message xiMessage)
    {
      var lReader = new StreamReader(xiMessage.BodyStream);
      return lReader.ReadToEnd();
    }

    private amBXScene DeserialiseScene(string xiEncodedScene)
    {
      var lXmlSerialiser = new XmlSerializer(typeof (amBXScene));
      var lReader = new StringReader(xiEncodedScene);
      return (amBXScene) lXmlSerialiser.Deserialize(lReader);
    }
  }
}
