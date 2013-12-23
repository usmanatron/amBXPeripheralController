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
      throw new NotImplementedException();
    }

    amBXScene mScene;
  }
}
