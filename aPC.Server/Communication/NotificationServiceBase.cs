using System.IO;
using System.Xml.Serialization;
using aPC.Common.Communication;
using aPC.Common.Entities;

namespace aPC.Common.Server.Communication
{
  public abstract class NotificationServiceBase : INotificationService
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

    protected abstract void UpdateScene(amBXScene xiScene);
  }
}
