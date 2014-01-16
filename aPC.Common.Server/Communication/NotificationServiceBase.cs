using System;
using System.IO;
using System.Xml.Serialization;
using aPC.Common.Accessors;
using aPC.Common.Communication;
using aPC.Common.Entities;

namespace aPC.Common.Server.Communication
{
  public abstract class NotificationServiceBase : INotificationService
  {
    public string RunCustomScene(string xiSceneXml)
    {
      try
      {
        var lScene = DeserialiseScene(xiSceneXml);
        UpdateScene(lScene);
      }
      catch (Exception e)
      {
        return e.ToString();
      }

      return "";
    }

    public string RunIntegratedScene(string xiSceneName)
    {
      try
      {
        var lAccessor = new SceneAccessor();
        var lScene = lAccessor.GetScene(xiSceneName) ?? 
                     lAccessor.GetScene("Error_Flash");

        UpdateScene(lScene);
      }
      catch (Exception e)
      {
        return e.ToString();
      }

      return "";
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
