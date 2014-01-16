using aPC.Common.Server.Applicators;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using amBXLib;
using System;

namespace aPC.Server
{
  class RumbleApplicator : ApplicatorBase<Rumble>
  {
    public RumbleApplicator(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new RumbleManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    protected override void ActNextFrame()
    {
      var lRumbleData = Manager.GetNext();

      if (lRumbleData != null)
      {
        Engine.UpdateRumble(mDirection, null);//qqUMI null
        WaitforInterval(lRumbleData.Length);
      }
      else
      {
        WaitforInterval(  1000); //qqUMI constantify
      }
    }

    private readonly CompassDirection mDirection;
  }
}
