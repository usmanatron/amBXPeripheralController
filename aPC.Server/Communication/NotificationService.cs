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

    public void RunScene(amBXScene scene)
    {
      action(scene);
    }

    public void RunSceneName(string sceneName)
    {
      var scene = sceneAccessor.GetScene(sceneName) ??
                   sceneAccessor.GetScene("Error_Flash");

      action(scene);
    }

    public string[] GetAvailableScenes()
    {
      return sceneAccessor.GetAllScenes()
        .Select(scene => scene.Key)
        .ToArray();
    }
  }
}