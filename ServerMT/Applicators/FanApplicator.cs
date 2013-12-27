using Common.Server.Applicators;
using Common.Entities;
using Common.Server.Managers;
using ServerMT.Managers;
using amBXLib;

namespace ServerMT.Applicators
{
  class FanApplicator : ApplicatorBase<Fan>
  {
    public FanApplicator(CompassDirection xiDirection, EngineManager xiEngine) 
      : base (xiEngine, new FanManager(xiDirection))
    {
      mDirection = xiDirection;
    }

    protected override void ActNextFrame()
    {
      var lFanData = Manager.GetNext();

      if (lFanData != null)
      {
        Engine.UpdateFan(mDirection, lFanData.Item);
      }

      WaitforInterval(lFanData.Length);
    }

    private CompassDirection mDirection;
  }
}
