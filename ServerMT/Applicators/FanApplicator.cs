using Common.Server.Applicators;
using Common.Entities;
using Common.Server.Managers;
using ServerMT.Managers;
using System;
using amBXLib;

namespace ServerMT.Applicators
{
  class FanApplicator : ApplicatorBase<Fan>
  {
    public FanApplicator(CompassDirection xiDirection, EngineManager xiEngine, Action xiEventCallback) 
      : base (xiEngine, new FanManager(xiDirection, xiEventCallback))
    {
      mDirection = xiDirection;
    }

    protected override void ActNextFrame()
    {
      var lFanData = Manager.GetNext();

      if (lFanData != null)
      {
        Engine.UpdateFan(mDirection, lFanData.Item);
        WaitforInterval(lFanData.Length);
      }
      else
      {
        WaitforInterval(1000); //qqUMI constantify
      }
    }

    private CompassDirection mDirection;
  }
}
