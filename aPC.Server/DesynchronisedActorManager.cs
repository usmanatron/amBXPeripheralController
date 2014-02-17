using amBXLib;
using aPC.Common.Server.EngineActors;
using aPC.Server.EngineActors;
using aPC.Server.Managers;
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
  class ComponentManagerCollection
  {
    public ComponentManagerCollection(EngineManager xiEngine, Action xiEventComplete)
    {
      SetupActors(xiEngine, xiEventComplete);
    }

    private void SetupActors(EngineManager xiEngine, Action xiAction)
    {
      mComponentManagers = new List<ComponentManager>();

      mComponentManagers.Add(new LightManager(eDirection.North, new LightActor(eDirection.North, xiEngine), xiAction));
      mComponentManagers.Add(new LightManager(eDirection.NorthEast, new LightActor(eDirection.NorthEast, xiEngine), xiAction));
      mComponentManagers.Add(new LightManager(eDirection.East, new LightActor(eDirection.East, xiEngine), xiAction));
      mComponentManagers.Add(new LightManager(eDirection.SouthEast, new LightActor(eDirection.SouthEast, xiEngine), xiAction));
      mComponentManagers.Add(new LightManager(eDirection.South, new LightActor(eDirection.South, xiEngine), xiAction));
      mComponentManagers.Add(new LightManager(eDirection.SouthWest, new LightActor(eDirection.SouthWest, xiEngine), xiAction));
      mComponentManagers.Add(new LightManager(eDirection.West, new LightActor(eDirection.West, xiEngine), xiAction));
      mComponentManagers.Add(new LightManager(eDirection.NorthWest, new LightActor(eDirection.NorthWest, xiEngine), xiAction));

      mComponentManagers.Add(new FanManager(eDirection.East, new FanActor(eDirection.East, xiEngine), xiAction));
      mComponentManagers.Add(new FanManager(eDirection.West, new FanActor(eDirection.West, xiEngine), xiAction));
      //qqUMI Check we use Center everywhere else this will blow up
      mComponentManagers.Add(new RumbleManager(eDirection.Center , new RumbleActor(eDirection.Center, xiEngine), xiAction));
    }


    public IEnumerable<ComponentManager> ActorsWithType(eComponentType xiComponentType)
    {
      return mComponentManagers
        .Where(manager => manager.ComponentType() == xiComponentType);
    }

    public List<ComponentManager> AllManagers()
    {
      return mComponentManagers;
    }

    private List<ComponentManager> mComponentManagers;
  }
}
