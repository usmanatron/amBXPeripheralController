using System;
using System.Collections.Generic;
using System.Threading;
using aPC.Common.Entities;
using aPC.Client.Disco.Generators;

namespace aPC.Client.Disco
{
  class DiscoTask
  {
    public DiscoTask(Settings xiSettings)
    {
      mSettings = xiSettings;
      mRandomSceneGenerator = new RandomSceneGenerator(mSettings);
    }

    public void Run()
    {
      while (true)
      {
        var lScene = GenerateScene();
        PushScene(lScene);
        WaitForInterval();
      }
    }

    private amBXScene GenerateScene()
    {
      //qqUMI 
      return new amBXScene();
    }

    private void PushScene(amBXScene xiScene)
    {
      //qqUMI
    }

    private void WaitForInterval()
    {
      Thread.Sleep(mSettings.PushInterval);
    }

    private RandomSceneGenerator mRandomSceneGenerator;
    private Settings mSettings;
  }
}
