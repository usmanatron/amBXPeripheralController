using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using amBXLib;
using System;

namespace aPC.Common.Server.Applicators
{
  public class FrameApplicator : ApplicatorBase<Frame>
  {
    public FrameApplicator(EngineManager xiEngine) 
      : base (xiEngine, new FrameManager())
    {
    }

    public FrameApplicator(EngineManager xiEngine, Action xiEventComplete)
      : base(xiEngine, new FrameManager(xiEventComplete))
    {
    }

    protected override void ActNextFrame()
    {
      var lFrameData = Manager.GetNext();
      var lFrame = lFrameData.Item;


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

      WaitforInterval(lFrame.Length);
    }

    private void UpdateLights(LightComponent xiLights)
    {
      Engine.UpdateLight(CompassDirection.North, xiLights.North, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.NorthEast, xiLights.NorthEast, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.East, xiLights.East, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.SouthEast, xiLights.SouthEast, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.South, xiLights.South, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.SouthWest, xiLights.SouthWest, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.West, xiLights.West, xiLights.FadeTime);
      Engine.UpdateLight(CompassDirection.NorthWest, xiLights.NorthWest, xiLights.FadeTime);
    }

    private void UpdateFans(FanComponent xiFans)
    {
      Engine.UpdateFan(CompassDirection.East, xiFans.East);
      Engine.UpdateFan(CompassDirection.West, xiFans.West);
    }

    private void UpdateRumbles(RumbleComponent xiInputRumble)
    {
      Engine.UpdateRumble(CompassDirection.Everywhere, xiInputRumble.Rumble);
    }
  }
}
