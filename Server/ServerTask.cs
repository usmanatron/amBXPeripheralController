﻿using Common.Server.Communication;
using Common.Server.Applicators;
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
