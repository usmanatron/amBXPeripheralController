using aPC.Common.Entities;
using aPC.Common.Server.Snapshots;
using System;

namespace aPC.Common.Server.SceneHandlers
{
  public class FanHandler : ComponentHandler<Fan>
  {
    public FanHandler(amBXScene xiScene, Action xiEventcomplete)
      : base(xiScene, xiEventcomplete)
    {
    }

    public override ComponentSnapshot<Fan> GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();
      var lFan = GetFan(xiDirection, lFrame.Fans);

      return lFan == null
        ? new ComponentSnapshot<Fan>(lFrame.Length)
        : new ComponentSnapshot<Fan>(lFan, lFrame.Fans.FadeTime, lFrame.Length);
    }

    private Fan GetFan(eDirection xiDirection, FanSection xiFans)
    {
      return xiFans.GetComponentValueInDirection(xiDirection);
    }
  }
}