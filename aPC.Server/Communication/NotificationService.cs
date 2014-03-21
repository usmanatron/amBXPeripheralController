using aPC.Common;
using aPC.Common.Communication;
using aPC.Common.Entities;
using aPC.Server.Communication;
using System.IO;
using System.Xml.Serialization;

namespace aPC.Server.Communication
{
  class NotificationService : INotificationService
  {
    public void RunCustomScene(string xiSceneXml)
    {
      var lScene = DeserialiseScene(xiSceneXml);
      UpdateScene(lScene);
    }

    public void RunIntegratedScene(string xiSceneName)
    {
      var lAccessor = new SceneAccessor();
      var lScene = lAccessor.GetScene(xiSceneName) ??
                   lAccessor.GetScene("Error_Flash");

      UpdateScene(lScene);
    }

    private amBXScene DeserialiseScene(string xiSceneXml)
    {
      using (var lReader = new StringReader(xiSceneXml))
      {
        var lSerialiser = new XmlSerializer(typeof(amBXScene));
        return (amBXScene)lSerialiser.Deserialize(lReader);
      }
    }

    protected void UpdateScene(amBXScene xiScene)
    {
      Server.ServerTask.Update(xiScene);
    }
  }
}
