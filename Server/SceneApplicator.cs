using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Server.Applicators;
using Common.Server.Managers;
using System.Threading;
using Common.Entities;
using amBXLib;

namespace Server
{
  public class SceneApplicator : ApplicatorBase<Frame>
  {
    public SceneApplicator(EngineManager xiEngine) : base (xiEngine, new SceneManager())
    {
    }

    protected override void ActNextFrame()
    {
      var lFrame = mManager.GetNext();

      if (lFrame.Lights != null)
      {
        UpdateLights(lFrame.Lights);
      }

      if (lFrame.Fans != null)
      {
        UpdateFans(lFrame.Fans);
      }

      if (lFrame.Rumble != null)
      {
        UpdateRumbles(lFrame.Rumble);
      }

      WaitforInterval(lFrame.Length);
    }

    private void UpdateLights(LightComponent xiLights)
    {
      mEngine.UpdateLight(CompassDirection.North, xiLights.North, xiLights.FadeTime);
      mEngine.UpdateLight(CompassDirection.NorthEast, xiLights.NorthEast, xiLights.FadeTime);
      mEngine.UpdateLight(CompassDirection.East, xiLights.East, xiLights.FadeTime);
      mEngine.UpdateLight(CompassDirection.SouthEast, xiLights.SouthEast, xiLights.FadeTime);
      mEngine.UpdateLight(CompassDirection.South, xiLights.South, xiLights.FadeTime);
      mEngine.UpdateLight(CompassDirection.SouthWest, xiLights.SouthWest, xiLights.FadeTime);
      mEngine.UpdateLight(CompassDirection.West, xiLights.West, xiLights.FadeTime);
      mEngine.UpdateLight(CompassDirection.NorthWest, xiLights.NorthWest, xiLights.FadeTime);
    }

    private void UpdateFans(FanComponent xiFans)
    {
      mEngine.UpdateFan(CompassDirection.East, xiFans.East);
      mEngine.UpdateFan(CompassDirection.West, xiFans.West);
    }

    private void UpdateRumbles(RumbleComponent xiInputRumble)
    {
      mEngine.UpdateRumble(CompassDirection.Everywhere, xiInputRumble);
    }
  }
}
