using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;

namespace aPC.Common.Server.Actors
{
  public class FrameActor : ActorBase<FrameSnapshot>
  {
    public FrameActor(IEngine engine)
      : base(engine)
    {
    }

    public override void ActNextFrame(eDirection direction, FrameSnapshot snapshot)
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
      Engine.UpdateLight(eDirection.North, lights.North, lights.FadeTime);
      Engine.UpdateLight(eDirection.NorthEast, lights.NorthEast, lights.FadeTime);
      Engine.UpdateLight(eDirection.East, lights.East, lights.FadeTime);
      Engine.UpdateLight(eDirection.SouthEast, lights.SouthEast, lights.FadeTime);
      Engine.UpdateLight(eDirection.South, lights.South, lights.FadeTime);
      Engine.UpdateLight(eDirection.SouthWest, lights.SouthWest, lights.FadeTime);
      Engine.UpdateLight(eDirection.West, lights.West, lights.FadeTime);
      Engine.UpdateLight(eDirection.NorthWest, lights.NorthWest, lights.FadeTime);
    }

    private void UpdateFans(FanSection fans)
    {
      Engine.UpdateFan(eDirection.East, fans.East);
      Engine.UpdateFan(eDirection.West, fans.West);
    }

    private void UpdateRumbles(RumbleSection rumble)
    {
      Engine.UpdateRumble(eDirection.Center, rumble.Rumble);
    }
  }
}