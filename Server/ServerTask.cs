using System.Threading;
using Common.Accessors;
using Server.Communication;

namespace Server
{
  class ServerTask
  {
    public ServerTask()
    {
      var lSceneAccessor = new SceneAccessor();
      // The "Default_RedVsBlue" scene definitely exists
      Manager = new amBXSceneManager(lSceneAccessor.GetScene("Default_RedVsBlue"));
    }

    public void Run()
    {
      using (new CommunicationManager()) 
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
      var lFrame = Manager.GetNextFrame();

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

    public static amBXSceneManager Manager;
  }
}
