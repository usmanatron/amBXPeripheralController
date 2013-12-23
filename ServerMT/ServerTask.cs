using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Server.Communication;
using ServerMT.Communication;
using System.Threading;
using amBXLib;
using Common.Server.Managers;
using System.ComponentModel;

namespace ServerMT
{
  class ServerTask
  {
    public ServerTask()
    {
      mLights = new Dictionary<CompassDirection, LightApplicator>();
      mFans = new Dictionary<CompassDirection, FanApplicator>();
    }

    public void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        SetupApplicators(lEngine);
        var lWorkers = new List<BackgroundWorker>();

        foreach (var lLight in mLights.Select(entry => entry.Value))
        {
          var lWorker = new BackgroundWorker();
          lWorker.DoWork += lLight.RunAsync;
          lWorkers.Add(lWorker);
        }

        Thread.Sleep(1000);

        foreach (var lWorker in lWorkers)
        {
          lWorker.RunWorkerAsync();
        }

        while (true)
        {
          Thread.Sleep(60 * 1000);
        }
      }
    }

    private void SetupApplicators(EngineManager xiEngine)
    {
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

    private Dictionary<CompassDirection, LightApplicator> mLights;
    private Dictionary<CompassDirection, FanApplicator> mFans;
    //private Dictionary<CompassDirection, RumbleApplicator> mRumbles;

  }
}