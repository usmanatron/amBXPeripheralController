using amBXLib;
using Common.Entities;
using Common.Integration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Server.Managers
{
  class LightManager : ManagerBase<Light>
  {
    public LightManager(EngineManagerBase xiEngine, CompassDirection xiDirection) 
      : base(new amBXScene())//qqUMI fix!
    {
      mComponentType = eComponentType.Light;
      mDirection = xiDirection;

      //qqUMI Setup a standard scene here by calling UpdateScene, like SceneManager
    }

    public override Light GetNext()
    {
      var lFrame = GetNextFrame();
      var lLight = CompassDirectionConverter.GetLight(mDirection, lFrame.Lights);

      return lLight;
    }

    eComponentType mComponentType;
    CompassDirection mDirection;
  }
}
