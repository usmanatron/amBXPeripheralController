using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client
{
  class Settings
  {
    public Settings(bool xiIsIntegratedScene, string xiSceneData)
    {
      IsIntegratedScene = xiIsIntegratedScene;
      SceneData = xiSceneData;
    }

    public readonly bool IsIntegratedScene;
    public readonly string SceneData;
  }
}
