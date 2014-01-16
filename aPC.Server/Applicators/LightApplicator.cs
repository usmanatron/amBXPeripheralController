using aPC.Common.Server.Applicators;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using aPC.Server.Managers;
using amBXLib;
using System;

namespace aPC.Server
{
  class LightApplicator : ApplicatorBase<Light>
  {
    public LightApplicator(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new LightManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    protected override void ActNextFrame()
    {
      var lLightData = Manager.GetNext();

      if (lLightData != null)
      {
        //Debug
        Console.WriteLine(mDirection + " - UpdateLight - " + DateTime.Now.Ticks);
        Engine.UpdateLight(mDirection, lLightData.Item, lLightData.FadeTime);
        WaitforInterval(lLightData.Length);
      }
      else
      {
        WaitforInterval(1000); //qqUMI constantify
      }
    }

    private readonly CompassDirection mDirection;
  }
}
