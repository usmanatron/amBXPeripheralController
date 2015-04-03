using aPC.Common.Entities;
using aPC.Common.Server.Engine;
using aPC.Common.Server.Snapshots;
using System.Linq;

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
      if (snapshot.Frame.LightSection != null)
      {
        UpdateLights(snapshot.Frame.LightSection);
      }

      if (snapshot.Frame.FanSection != null)
      {
        UpdateFans(snapshot.Frame.FanSection);
      }

      if (snapshot.Frame.RumbleSection != null)
      {
        UpdateRumbles(snapshot.Frame.RumbleSection);
      }
    }

    private void UpdateLights(LightSection lights)
    {
      engine.UpdateComponent(eDirection.North, lights.GetComponentValueInDirection(eDirection.North));
      engine.UpdateComponent(eDirection.NorthEast, lights.GetComponentValueInDirection(eDirection.NorthEast));
      engine.UpdateComponent(eDirection.East, lights.GetComponentValueInDirection(eDirection.East));
      engine.UpdateComponent(eDirection.SouthEast, lights.GetComponentValueInDirection(eDirection.SouthEast));
      engine.UpdateComponent(eDirection.South, lights.GetComponentValueInDirection(eDirection.South));
      engine.UpdateComponent(eDirection.SouthWest, lights.GetComponentValueInDirection(eDirection.SouthWest));
      engine.UpdateComponent(eDirection.West, lights.GetComponentValueInDirection(eDirection.West));
      engine.UpdateComponent(eDirection.NorthWest, lights.GetComponentValueInDirection(eDirection.NorthWest));
    }

    private void UpdateFans(FanSection fans)
    {
      engine.UpdateComponent(eDirection.East, fans.GetComponentValueInDirection(eDirection.East));
      engine.UpdateComponent(eDirection.West, fans.GetComponentValueInDirection(eDirection.West));
    }

    private void UpdateRumbles(RumbleSection rumble)
    {
      engine.UpdateComponent(eDirection.Center, rumble.Rumbles.Single());
    }
  }
}