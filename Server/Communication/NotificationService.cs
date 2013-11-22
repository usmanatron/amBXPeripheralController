using System;
using Common;
using Common.Communication;
using Common.Entities;

namespace Server.Communication
{
  class NotificationService : INotificationService
  {
    public string RunCustomScene(amBXScene xiScene)
    {
      try
      {
        UpdateScene(xiScene);
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
        // The following line will always find a scene - if there is an error
        // it defaults to "Lights off".
        var lScene = new IntegratedamBXSceneAccessor().GetScene(xiSceneName);
        UpdateScene(lScene);
      }
      catch (Exception e)
      {
        return e.ToString();
      }

      return "";
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
