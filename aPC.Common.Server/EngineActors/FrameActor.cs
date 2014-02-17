using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using amBXLib;
using System;

namespace aPC.Common.Server.EngineActors
{
  public class FrameActor : EngineActorBase
  {
    public FrameActor(EngineManager xiEngine) 
      : base (xiEngine)
    {
    }

    public override void ActNextFrame(Snapshot xiFrameData)
    {
      var lFrameData = (FrameSnapshot)xiFrameData;
      var lFrame = lFrameData.Frame;

      if (lFrame.Lights != null)
      {
        UpdateLights(lFrame.Lights);
      }

      if (lFrame.Fans != null)
      {
        UpdateFans(lFrame.Fans);
      }

      if (lFrame.Rumbles != null)
      {
        UpdateRumbles(lFrame.Rumbles);
      }
    }

    private void UpdateLights(LightSection xiLights)
    {
      Engine.UpdateLight(eDirection.North, xiLights.North, xiLights.FadeTime);
      Engine.UpdateLight(eDirection.NorthEast, xiLights.NorthEast, xiLights.FadeTime);
      Engine.UpdateLight(eDirection.East, xiLights.East, xiLights.FadeTime);
      Engine.UpdateLight(eDirection.SouthEast, xiLights.SouthEast, xiLights.FadeTime);
      Engine.UpdateLight(eDirection.South, xiLights.South, xiLights.FadeTime);
      Engine.UpdateLight(eDirection.SouthWest, xiLights.SouthWest, xiLights.FadeTime);
      Engine.UpdateLight(eDirection.West, xiLights.West, xiLights.FadeTime);
      Engine.UpdateLight(eDirection.NorthWest, xiLights.NorthWest, xiLights.FadeTime);
    }

    private void UpdateFans(FanSection xiFans)
    {
      Engine.UpdateFan(eDirection.East, xiFans.East);
      Engine.UpdateFan(eDirection.West, xiFans.West);
    }

    private void UpdateRumbles(RumbleSection xiInputRumble)
    {
      Engine.UpdateRumble(eDirection.Center, xiInputRumble.Rumble);
    }
  }
}
