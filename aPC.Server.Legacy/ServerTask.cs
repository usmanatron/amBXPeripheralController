using aPC.Common.Server.Communication;
using aPC.Common.Server.EngineActors;
using aPC.Common.Server.Managers;
using aPC.Server.Legacy.Communication;

namespace aPC.Server.Legacy
{
  class ServerTask
  {
    public void Run()
    {
      using (new CommunicationManager(new NotificationService())) 
      using (var lEngine = new EngineManager())
      {
        Actor = new FrameActor(lEngine);
        while (true)
        {
          Actor.Run();
        }
      }
    }

    internal static FrameActor Actor;
  }
}
