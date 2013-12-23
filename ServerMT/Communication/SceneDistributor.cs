using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Common.Entities;

namespace ServerMT.Communication
{
  class SceneDistributor
  {
    public SceneDistributor(amBXScene xiScene)
    {
      mScene = xiScene;
    }

    public void Distribute()
    {
      foreach (var lLight in ServerTask.Lights)
      {
        lLight.Value.UpdateManager(mScene);
      }

      foreach (var lFan in ServerTask.Fans)
      {
        lFan.Value.UpdateManager(mScene);
      }

      //qqUMI Rumble not supported yet
    }

    amBXScene mScene;
  }
}
