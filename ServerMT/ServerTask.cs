using System.Collections.Generic;
using Common.Server.Communication;
using ServerMT.Communication;
using System.Linq;
using System.Threading.Tasks;
using amBXLib;
using Common.Server.Managers;
using Common.Server.Applicators;
using ServerMT.Applicators;

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

        // Start them and watch
        while (true)
        {
          if (RunSynchronised())
          {
            Frame.Run();
          }
          else
          {
            Parallel.ForEach(Lights.Select(light => light.Value), light => light.Run());
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