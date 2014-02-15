using amBXLib;
using aPC.Common;
using aPC.Common.Server.EngineActors;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Server
{
  class DesynchronisedActor
  {
    public DesynchronisedActor(eDirection xiDirection, EngineActorBase xiActor)
    {
      mDirection = xiDirection;
      Actor = xiActor;
    }

    public eActorType ActorType
    {
      get
      {
        return Actor.ActorType();
      }
    }

    public EngineActorBase Actor;
    private eDirection mDirection; 
  }
}
