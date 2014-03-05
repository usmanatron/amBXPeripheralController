using aPC.Common;
using aPC.Common.Entities;
using aPC.Common.Server.SceneHandlers;
using aPC.Common.Server.Snapshots;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.SceneHandlers
{
  class RumbleHandler : ComponentHandler<Rumble>
  {
    public RumbleHandler(amBXScene xiScene, Action xiEventcomplete) : base(xiScene, xiEventcomplete)
    { 
    }

    public override ComponentSnapshot<Rumble> GetNextSnapshot(eDirection xiDirection)
    {
      var lFrame = GetNextFrame();

      Rumble lRumble = lFrame.Rumbles == null
        ? null
        : lFrame.Rumbles.Rumble;

      return lRumble == null
        ? new ComponentSnapshot<Rumble>(lFrame.Length)
        : new ComponentSnapshot<Rumble>(lRumble, lFrame.Rumbles.FadeTime, lFrame.Length);
    }
  }
}
