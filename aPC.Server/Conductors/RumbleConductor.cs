using aPC.Common.Entities;
using aPC.Common.Server.Conductors;
using aPC.Common.Server.Snapshots;
using aPC.Common;
using System.Linq;
using System;
using System.Collections.Generic;
using aPC.Server.EngineActors;
using aPC.Server.SceneHandlers;

namespace aPC.Server.Conductors
{
  class RumbleConductor : ComponentConductor<Rumble>
  {
    public RumbleConductor(RumbleActor xiActor, RumbleHandler xiHandler) 
      : base(xiActor, xiHandler)
    {
    }

    public override eComponentType ComponentType()
    {
      return eComponentType.Rumble;
    }
  }
}
