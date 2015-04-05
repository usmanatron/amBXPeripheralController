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
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.North));
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.NorthEast));
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.East));
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.SouthEast));
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.South));
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.SouthWest));
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.West));
      engine.UpdateComponent(lights.GetComponentSectionInDirection(eDirection.NorthWest));
    }

    private void UpdateFans(FanSection fans)
    {
      engine.UpdateComponent(fans.GetComponentSectionInDirection(eDirection.East));
      engine.UpdateComponent(fans.GetComponentSectionInDirection(eDirection.West));
    }

    private void UpdateRumbles(RumbleSection rumble)
    {
      engine.UpdateComponent(rumble.Rumbles.Single());
    }
  }
}