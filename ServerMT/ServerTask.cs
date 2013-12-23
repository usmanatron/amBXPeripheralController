using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Server.Communication;
using ServerMT.Communication;
using System.Threading;
using amBXLib;

namespace ServerMT
{
  class ServerTask
  {

    public void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        SetupManagers(lEngine);
        while(true)
        {
          Thread.Sleep(10000);
        }

      }
    }

    private void SetupManagers(EngineManager xiEngine)
    {
    }
  }
}
