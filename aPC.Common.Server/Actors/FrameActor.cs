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

    public void ActNextFrame(FrameSnapshot snapshot)
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
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.North));
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.NorthEast));
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.East));
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.SouthEast));
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.South));
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.SouthWest));
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.West));
      engine.UpdateComponent(lights.GetComponentValueInDirection(eDirection.NorthWest));
    }

    private void UpdateFans(FanSection fans)
    {
      engine.UpdateComponent(fans.GetComponentValueInDirection(eDirection.East));
      engine.UpdateComponent(fans.GetComponentValueInDirection(eDirection.West));
    }

    private void UpdateRumbles(RumbleSection rumble)
    {
      engine.UpdateComponent(rumble.Rumbles.Single());
    }
  }
}