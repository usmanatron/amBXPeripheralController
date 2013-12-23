using System.Threading;
using Common.Server.Communication;
using Common.Server.Managers;
using Server.Communication;

namespace Server
{
  class ServerTask
  {
    public void Run()
    {
      using (new CommunicationManager(new NotificationService())) 
      using (var lEngine = new EngineManager())
      {
        var lApplicator = new SceneApplicator(lEngine);

        while (true)
        {
          lApplicator.Apply();
        }
      }
    }
  }
}
