using amBXLib;
using aPC.Common.Server.EngineActors;
using aPC.Server.EngineActors;
using aPC.Common.Server.Managers;
using aPC.Common.Entities;
using aPC.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Server
{
  class DesynchronisedActorManager
  {
    public DesynchronisedActorManager(EngineManager xiEngine, Action xiEventComplete)
    {
      SetupActors(xiEngine, xiEventComplete);
    }

    private void SetupActors(EngineManager xiEngine, Action xiAction)
    {
      mDesynchronisedActors = new List<DesynchronisedActor>();

      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.North,     new LightActor(eDirection.North, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.NorthEast, new LightActor(eDirection.NorthEast, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.East,      new LightActor(eDirection.East, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.SouthEast, new LightActor(eDirection.SouthEast, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.South,     new LightActor(eDirection.South, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.SouthWest, new LightActor(eDirection.SouthWest, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.West,      new LightActor(eDirection.West, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.NorthWest, new LightActor(eDirection.NorthWest, xiEngine, xiAction)));

      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.East, new FanActor(eDirection.East, xiEngine, xiAction)));
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.West, new FanActor(eDirection.West, xiEngine, xiAction)));
                                                        
      mDesynchronisedActors.Add(new DesynchronisedActor(eDirection.Everywhere, new RumbleActor(eDirection.Everywhere, xiEngine, xiAction)));
    }

    public List<DesynchronisedActor> ActorsWithType(eActorType xiActorType)
    {
      return mDesynchronisedActors
        .Where(actor => actor.ActorType == xiActorType)
        .ToList();
    }

    public List<DesynchronisedActor> AllActors()
    {
      return mDesynchronisedActors;
    }

    private List<DesynchronisedActor> mDesynchronisedActors;
  }
}
