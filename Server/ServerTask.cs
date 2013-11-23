using System.ServiceModel;
using System.Threading;
using Common.Accessors;
using Common.Communication;
using Server.Communication;

namespace Server
{
  class ServerTask
  {
    public ServerTask()
    {
      SetupAccess();
      var lSceneAccessor = new SceneAccessor();
      Manager = new amBXSceneManager(lSceneAccessor.GetScene("Default_RedVsBlue"));
    }

    private void SetupAccess()
    {
      mHost = new ServiceHost(typeof (NotificationService));
      mHost.AddServiceEndpoint(typeof (INotificationService), 
                               new BasicHttpBinding(),
                               CommunicationSettings.ServiceUrl);
      mHost.Open();
    }

    public void Run()
    {
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
        xiEngine.UpdateRumble(lFrame.Rumble);
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
    private ServiceHost mHost;
  }
}
