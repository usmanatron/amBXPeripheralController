using aPC.Common;
using aPC.Common.Communication;
using aPC.Common.Entities;
using Ninject;
using System;
using System.Linq;
using System.ServiceModel;

namespace aPC.Server.Communication
{
  [ServiceBehavior(InstanceContextMode = InstanceContextMode.Single, IncludeExceptionDetailInFaults = true)]
  public class NotificationService : INotificationService
  {
    private readonly Action<amBXScene> action;
    private readonly SceneAccessor sceneAccessor;

    [Inject]
    public NotificationService(SceneAccessor sceneAccessor)
      : this(sceneAccessor, ServerTask.Update)
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

    public ServerRegistrationResult RegisterWithServer(string id)
    {
      //TODO Add the correct logic
      return new ServerRegistrationResult()
      {
        Successful = true
      };
    }

    public void RunFrameExclusive(Frame frame)
    {
      ServerTask.UpdateExclusive(frame);
    }
  }
}