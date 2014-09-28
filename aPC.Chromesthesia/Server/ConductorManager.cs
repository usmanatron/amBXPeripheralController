using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Actors;
using aPC.Common.Server.SceneHandlers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace aPC.Chromesthesia.Server
{
  /* Manages the Chromesthesia Conductors.
   * This application only runs lights in Desync mode, so the sync \ event cases are not handled here.  See aPC.Server for a Manager that handles multiple cases
   * TODO: Consider merging these two classes together
   */
  class ConductorManager
  {
    private List<IConductor> lights;

    public ConductorManager(IEngine xiEngine, SceneAccessor sceneAccessor)
    {
      lights = new List<IConductor>();
      var scene = sceneAccessor.GetScene("rainbow");
      SetupManagers(xiEngine, scene, new Action(EventComplete));
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
      lights.Add(new LightConductor(eDirection.North, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      lights.Add(new LightConductor(eDirection.NorthEast, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      lights.Add(new LightConductor(eDirection.East, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      lights.Add(new LightConductor(eDirection.SouthEast, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      lights.Add(new LightConductor(eDirection.South, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      lights.Add(new LightConductor(eDirection.SouthWest, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      lights.Add(new LightConductor(eDirection.West, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));
      lights.Add(new LightConductor(eDirection.NorthWest, new LightActor(xiEngine), new LightHandler(xiScene, xiAction)));

      lights.ForEach(conductor => EnableAndRunIfRequired(conductor));
    }

    private void EnableAndRunIfRequired(IConductor xiConductor)
    {
      xiConductor.Enable();

      if (!xiConductor.IsRunning.Get)
      {
        ThreadPool.QueueUserWorkItem(_ => xiConductor.Run());
      }
    }

    public void Update(amBXScene xiScene)
    {
      Parallel.ForEach(lights, conductor => UpdateSceneIfRelevant(conductor, xiScene));
    }

    private void UpdateSceneIfRelevant(IConductor xiConductor, amBXScene xiScene)
    {
      if (IsApplicableForConductor(xiScene.FrameStatistics, xiConductor.ComponentType, xiConductor.Direction))
      {
        xiConductor.UpdateScene(xiScene);
      }
    }

    private Func<FrameStatistics, eComponentType, eDirection, bool> IsApplicableForConductor =
      (statistics, componentType, direction) => statistics.AreEnabledForComponentAndDirection(componentType, direction);
  }
}
