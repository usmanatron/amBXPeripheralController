using System.Threading;
using System.Threading.Tasks;
using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;
using System;

namespace aPC.Chromesthesia.Server
{
  /* Manages the Chromesthesia Conductors.
   * This application only runs lights in Sync mode, so the desync \ event cases are not handled here.
   * See aPC.Server for a Manager that handles multiple cases
   * TODO: Consider merging these two classes together
   */
  class ConductorManager
  {
    private IConductor conductor;

    public ConductorManager(IEngine xiEngine, SceneAccessor sceneAccessor)
    {
      var scene = sceneAccessor.GetScene("rainbow");
      SetupManagers(xiEngine, scene, EventComplete);
    }

    /// <summary>
    /// The action to take when an event has been run
    /// </summary>
    /// <remarks>
    ///   As events are not supported, we never expect this to be called, therefore throw an exception!
    /// </remarks>
    private void EventComplete()
    {
      throw new InvalidOperationException("Attempted to run an event through the Chromesthesia application - this should never happen!");
    }

    private void SetupManagers(IEngine xiEngine, amBXScene xiScene, Action xiAction)
    {
      conductor = new FrameConductor(new FrameActor(xiEngine), new FrameHandler(xiScene, xiAction));
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
