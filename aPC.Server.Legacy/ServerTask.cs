using aPC.Common.Server.Communication;
using aPC.Common.Server.Applicators;
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
        Applicator = new FrameApplicator(lEngine);
        while (true)
        {
          Applicator.Run();
        }
      }
    }

    internal static FrameApplicator Applicator;
  }
}
