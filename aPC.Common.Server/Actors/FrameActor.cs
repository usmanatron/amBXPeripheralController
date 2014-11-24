using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class FrameActor : IActor<FrameSnapshot>
  {
    private IEngine engine;

    public FrameActor(IEngine engine)
    {
      this.engine = engine;
    }

    public void ActNextFrame(eDirection direction, FrameSnapshot snapshot)
    {
      if (snapshot.Frame.Lights != null)
      {
        UpdateLights(snapshot.Frame.Lights);
      }

      if (snapshot.Frame.Fans != null)
      {
        UpdateFans(snapshot.Frame.Fans);
      }

      if (snapshot.Frame.Rumbles != null)
      {
        UpdateRumbles(snapshot.Frame.Rumbles);
      }
    }

    private void UpdateLights(LightSection lights)
    {
      engine.UpdateComponent(eDirection.North, lights.North);
      engine.UpdateComponent(eDirection.NorthEast, lights.NorthEast);
      engine.UpdateComponent(eDirection.East, lights.East);
      engine.UpdateComponent(eDirection.SouthEast, lights.SouthEast);
      engine.UpdateComponent(eDirection.South, lights.South);
      engine.UpdateComponent(eDirection.SouthWest, lights.SouthWest);
      engine.UpdateComponent(eDirection.West, lights.West);
      engine.UpdateComponent(eDirection.NorthWest, lights.NorthWest);
    }

    private void UpdateFans(FanSection fans)
    {
      engine.UpdateComponent(eDirection.East, fans.East);
      engine.UpdateComponent(eDirection.West, fans.West);
    }

    private void UpdateRumbles(RumbleSection rumble)
    {
      engine.UpdateComponent(eDirection.Center, rumble.Rumble);
    }
  }
}