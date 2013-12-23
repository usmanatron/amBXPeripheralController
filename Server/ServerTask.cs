using System.Threading;
using Common.Accessors;
using Common.Server.Communication;
using Common.Server.Managers;
using Server.Communication;

namespace Server
{
  class ServerTask
  {
    public ServerTask()
    {
      var lSceneAccessor = new SceneAccessor();
      // The "Default_RedVsBlue" scene definitely exists
      Manager = new SceneManager(lSceneAccessor.GetScene("Default_RedVsBlue"));
    }

    public void Run()
    {
      using (new CommunicationManager(new NotificationService())) 
      using (var lEngine = new EngineManager())
      {
        while (true)
        {
          lock (Manager)
          {
            ActNextFrame(lEngine);
            AdvanceScene();
          }
        }
      }
    }

    private void ActNextFrame(EngineManager xiEngine)
    {
      var lFrame = Manager.GetNext();

      if (lFrame.Lights != null)
      {
        xiEngine.UpdateLights(lFrame.Lights);
      }

      if (lFrame.Fans != null)
      {
        xiEngine.UpdateFans(lFrame.Fans);
      }

      if (lFrame.Rumble != null)
      {
        xiEngine.UpdateRumbles(lFrame.Rumble);
      }

      WaitforInterval(lFrame.Length);
    }


    private void WaitforInterval(int xiLength)
    {
      Thread.Sleep(xiLength);
    }

    private void AdvanceScene()
    {
      Manager.AdvanceScene();
    }

    public static SceneManager Manager;
  }
}
