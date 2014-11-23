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
      engine.UpdateComponent(eDirection.North, lights.North, lights.FadeTime);
      engine.UpdateComponent(eDirection.NorthEast, lights.NorthEast, lights.FadeTime);
      engine.UpdateComponent(eDirection.East, lights.East, lights.FadeTime);
      engine.UpdateComponent(eDirection.SouthEast, lights.SouthEast, lights.FadeTime);
      engine.UpdateComponent(eDirection.South, lights.South, lights.FadeTime);
      engine.UpdateComponent(eDirection.SouthWest, lights.SouthWest, lights.FadeTime);
      engine.UpdateComponent(eDirection.West, lights.West, lights.FadeTime);
      engine.UpdateComponent(eDirection.NorthWest, lights.NorthWest, lights.FadeTime);
    }

    private void UpdateFans(FanSection fans)
    {
      engine.UpdateComponent(eDirection.East, fans.East, fans.FadeTime);
      engine.UpdateComponent(eDirection.West, fans.West, fans.FadeTime);
    }

    private void UpdateRumbles(RumbleSection rumble)
    {
      engine.UpdateComponent(eDirection.Center, rumble.Rumble, rumble.FadeTime);
    }
  }
}