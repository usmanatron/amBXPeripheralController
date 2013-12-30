using System.Collections.Generic;
using Common.Server.Communication;
using ServerMT.Communication;
using System.Linq;
using System.Threading.Tasks;
using amBXLib;
using Common.Server.Managers;
using Common.Server.Applicators;
using ServerMT.Applicators;
using Common.Entities;

namespace ServerMT
{
  class ServerTask
  {
    public ServerTask()
    {
      mLights = new Dictionary<CompassDirection, LightApplicator>();
      mFans = new Dictionary<CompassDirection, FanApplicator>();
    }

    internal void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        SetupApplicators(lEngine);

        // Start them and watch
        while (true)
        {
          if (RunSynchronised())
          {
            mFrame.Run();
          }
          else
          {
            Parallel.ForEach(mLights.Select(light => light.Value), light => light.Run());
          }
        }
      }
    }

    private bool RunSynchronised()
    {
      return true;
    }

    private void SetupApplicators(EngineManager xiEngine)
    {
      mFrame = new FrameApplicator(xiEngine);

      mLights.Add(CompassDirection.North,     new LightApplicator(CompassDirection.North, xiEngine));
      mLights.Add(CompassDirection.NorthEast, new LightApplicator(CompassDirection.NorthEast, xiEngine));
      mLights.Add(CompassDirection.East,      new LightApplicator(CompassDirection.East, xiEngine));
      mLights.Add(CompassDirection.SouthEast, new LightApplicator(CompassDirection.SouthEast, xiEngine));
      mLights.Add(CompassDirection.South,     new LightApplicator(CompassDirection.South, xiEngine));
      mLights.Add(CompassDirection.SouthWest, new LightApplicator(CompassDirection.SouthWest, xiEngine));
      mLights.Add(CompassDirection.West,      new LightApplicator(CompassDirection.West, xiEngine));
      mLights.Add(CompassDirection.NorthWest, new LightApplicator(CompassDirection.NorthWest, xiEngine));

      mFans.Add(CompassDirection.East, new FanApplicator(CompassDirection.East, xiEngine));
      mFans.Add(CompassDirection.West, new FanApplicator(CompassDirection.West, xiEngine));

      //qqUMI Add Rumble here
    }



    /*qqUMI
 * 
 * There are a number of issues here:
 * * If we're in de-sync mode and run a synchronised event, we'll fall back to whatever the 
 *   --synchronised-- previous scene was - not ideal
 * * If we go from sync to de-sync (with, say, a de-sync scene with only one light defined), then the
 *   other lights will stop animating.  Ideally we want to copy this all to the others
 *   
 * Fixing this may just be a case of changing how UpdateManager works a little bit to allow
 * us to force a value for IsDormant?
 */
    internal void Update(amBXScene xiScene, amBXScene xiEmptyScene)
    {
      if (xiScene.IsSynchronised)
      {
        UpdateSynchronisedApplicator(xiScene);
        UpdateUnsynchronisedElements(xiEmptyScene);
      }
      else
      {
        UpdateUnsynchronisedElements(xiScene);
        UpdateSynchronisedApplicator(xiEmptyScene);
      }
    }

    private void UpdateSynchronisedApplicator(amBXScene xiScene)
    {
      mFrame.UpdateManager(xiScene);
    }

    private void UpdateUnsynchronisedElements(amBXScene xiScene)
    {
      foreach (var lLight in mLights)
      {
        lLight.Value.UpdateManager(xiScene);
      }

      foreach (var lFan in mFans)
      {
        lFan.Value.UpdateManager(xiScene);
      }

      //qqUMI Rumble not supported yet
    }


    private FrameApplicator mFrame;
    
    private Dictionary<CompassDirection, LightApplicator> mLights;
    private Dictionary<CompassDirection, FanApplicator> mFans;
    //private Dictionary<CompassDirection, RumbleApplicator> mRumbles;
  }
}