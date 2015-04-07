using aPC.Common;
using aPC.Common.Communication;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace aPC.Server.Communication
{
  public class NotificationService : INotificationService
  {
    private readonly Action<amBXScene> action;

    public NotificationService()
      : this(ServerTask.UpdateScene)
    {
    }

    // Used for tests
    public NotificationService(Action<amBXScene> updateScene)
    {
      action = updateScene;
    }

    public void RunCustomScene(string sceneXml)
    {
      var scene = DeserialiseScene(sceneXml);
      UpdateScene(scene);
    }

    public void RunIntegratedScene(string sceneName)
    {
      var accessor = new SceneAccessor(new DefaultScenes());
      var scene = accessor.GetScene(sceneName) ??
                   accessor.GetScene("Error_Flash");

      UpdateScene(scene);
    }

    public string[] GetSupportedIntegratedScenes()
    {
      var lAccessor = new SceneAccessor(new DefaultScenes());

      return lAccessor.GetAllScenes()
        .Select(scene => scene.Key)
        .ToArray();
    }

    private amBXScene DeserialiseScene(string sceneXml)
    {
      using (var reader = new StringReader(sceneXml))
      {
        var serialiser = new XmlSerializer(typeof(amBXScene));
        return (amBXScene)serialiser.Deserialize(reader);
      }
    }

    protected void UpdateScene(amBXScene scene)
    {
      action(scene);
    }
  }
}