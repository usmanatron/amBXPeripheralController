using Common.Server.Applicators;
using Common.Entities;
using Common.Server.Managers;
using ServerMT.Managers;
using amBXLib;
using System;

namespace ServerMT
{
  class LightApplicator : ApplicatorBase<Light>
  {
    public LightApplicator(CompassDirection xiDirection, EngineManager xiEngine) 
      : base (xiEngine, new LightManager(xiDirection))
    {
      mDirection = xiDirection;
    }

    protected override void ActNextFrame()
    {
      var lLightData = mManager.GetNext();

      if (lLightData != null)
      {
        //Debug
        Console.WriteLine(mDirection + " - UpdateLight - " + DateTime.Now.Ticks);
        mEngine.UpdateLight(mDirection, lLightData.Item, lLightData.FadeTime);
      }

      WaitforInterval(lLightData.Length);
    }

    private CompassDirection mDirection;
  }
}
