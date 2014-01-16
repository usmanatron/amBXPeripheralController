﻿using aPC.Common.Server.Applicators;
using aPC.Common.Entities;
using aPC.Common.Server.Managers;
using Server.Managers;
using System;
using amBXLib;

namespace Server.Applicators
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
