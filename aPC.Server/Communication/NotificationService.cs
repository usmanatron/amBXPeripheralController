using aPC.Common;
using aPC.Common.Communication;
using aPC.Common.Entities;
using Ninject;
using System;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Xml.Serialization;

namespace aPC.Server.Communication
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single)]
  public class NotificationService : INotificationService
  {
    private readonly Action<amBXScene> action;
    private readonly SceneAccessor sceneAccessor;

    [Inject]
    public NotificationService(SceneAccessor sceneAccessor)
      : this(sceneAccessor, ServerTask.UpdateScene)
    {
    }

    // Used for tests
    public NotificationService(SceneAccessor sceneAccessor, Action<amBXScene> updateScene)
    {
      this.sceneAccessor = sceneAccessor;
      action = updateScene;
    }

    public void RunCustomScene(string sceneXml)
    {
      var scene = DeserialiseScene(sceneXml);
      UpdateScene(scene);
    }

    public void RunIntegratedScene(string sceneName)
    {
      var scene = sceneAccessor.GetScene(sceneName) ??
                   sceneAccessor.GetScene("Error_Flash");

      UpdateScene(scene);
    }

    public string[] GetSupportedIntegratedScenes()
    {
      return sceneAccessor.GetAllScenes()
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