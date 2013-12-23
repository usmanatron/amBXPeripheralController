using System.Collections.Generic;
using Common.Server.Applicators;
using Common.Entities;
using Common.Server.Managers;
using amBXLib;

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
        mEngine.UpdateLight(mDirection, lLightData.Item, lLightData.FadeTime);
      }

      WaitforInterval(lLightData.Length);
    }

    private CompassDirection mDirection;
  }
}
