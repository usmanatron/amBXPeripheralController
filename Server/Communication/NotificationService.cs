using System;
using System.IO;
using System.Xml.Serialization;
using Common.Accessors;
using Common.Communication;
using Common.Entities;

namespace Server.Communication
{
  class NotificationService : INotificationService
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

        var lScene = lAccessor.GetScene(xiSceneName);
        if (lScene == null)
        {
          // We failed to find the scene - show the error visually
          lScene = lAccessor.GetScene("Error_Flash");
        }

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

    private void UpdateScene(amBXScene xiScene)
    {
      lock (ServerTask.Manager)
      {
        ServerTask.Manager.UpdateScene(xiScene);
      }
    }
  }
}
