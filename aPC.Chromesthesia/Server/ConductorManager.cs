using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using System.Threading;

namespace aPC.Chromesthesia.Server
{
  /// <summary>
  /// Manages the Chromesthesia Conductors.
  /// This application only runs lights in Sync mode, so the desync \ event cases are not handled here.
  /// See aPC.Server for a Manager that handles multiple cases
  /// </summary>
  internal class ConductorManager
  {
    private IConductor conductor;

    public ConductorManager(FrameConductor frameConductor)
    {
      conductor = frameConductor;
      EnableAndRunConductor();
    }

    private void EnableAndRunConductor()
    {
      conductor.Enable();
      ThreadPool.QueueUserWorkItem(_ => conductor.Run());
    }

    public void Update(amBXScene xiScene)
    {
      conductor.UpdateScene(xiScene);
    }
  }
}