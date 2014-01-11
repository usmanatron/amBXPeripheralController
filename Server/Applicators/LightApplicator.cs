using Common.Server.Applicators;
using Common.Entities;
using Common.Server.Managers;
using Server.Managers;
using amBXLib;
using System;

namespace Server
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
