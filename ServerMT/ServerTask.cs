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
using Common.Server.Applicators;
using System.ComponentModel;

namespace ServerMT
{
  class ServerTask
  {
    public ServerTask()
    {
      Lights = new Dictionary<CompassDirection, LightApplicator>();
      Fans = new Dictionary<CompassDirection, FanApplicator>();
    }

    public void Run()
    {
      using (new CommunicationManager(new NotificationService()))
      using (var lEngine = new EngineManager())
      {
        SetupApplicators(lEngine);
        ThreadPool.QueueUserWorkItem(_ => Frame.RunAsync());
        Parallel.ForEach(Lights.Select(light => light.Value), light => { light.RunAsync(); });

        while (true)
        {
          Thread.Sleep(1000);
        }
      }
    }

    private void SetupApplicators(EngineManager xiEngine)
    {
      Frame = new FrameApplicator(xiEngine);

      Lights.Add(CompassDirection.North,     new LightApplicator(CompassDirection.North, xiEngine));
      Lights.Add(CompassDirection.NorthEast, new LightApplicator(CompassDirection.NorthEast, xiEngine));
      Lights.Add(CompassDirection.East,      new LightApplicator(CompassDirection.East, xiEngine));
      Lights.Add(CompassDirection.SouthEast, new LightApplicator(CompassDirection.SouthEast, xiEngine));
      Lights.Add(CompassDirection.South,     new LightApplicator(CompassDirection.South, xiEngine));
      Lights.Add(CompassDirection.SouthWest, new LightApplicator(CompassDirection.SouthWest, xiEngine));
      Lights.Add(CompassDirection.West,      new LightApplicator(CompassDirection.West, xiEngine));
      Lights.Add(CompassDirection.NorthWest, new LightApplicator(CompassDirection.NorthWest, xiEngine));

      Fans.Add(CompassDirection.East, new FanApplicator(CompassDirection.East, xiEngine));
      Fans.Add(CompassDirection.West, new FanApplicator(CompassDirection.West, xiEngine));

      //qqUMI Add Rumble here
    }

    public static FrameApplicator Frame;

    public static Dictionary<CompassDirection, LightApplicator> Lights;
    public static Dictionary<CompassDirection, FanApplicator> Fans;
    //private Dictionary<CompassDirection, RumbleApplicator> mRumbles;

  }
}