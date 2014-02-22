using System;
using System.Threading;
using aPC.Client.Disco.Generators;
using aPC.Client.Disco.Communication;
using aPC.Common;
using aPC.Common.Entities;

namespace aPC.Client.Disco
{
  class DiscoTask
  {
    public DiscoTask(Settings xiSettings, INotificationClient xiNotificationService)
    {
      mSettings = xiSettings;
      mRandom = new Random();
      mRandomSceneGenerator = new RandomSceneGenerator(mSettings, mRandom);
      mNotificationService = xiNotificationService;
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
      return mRandomSceneGenerator.Get();
    }

    private void PushScene(amBXScene xiScene)
    {
      mNotificationService.PushCustomScene(xiScene);
    }

    private void WaitForInterval()
    {
      Thread.Sleep(mSettings.PushInterval);
    }
    
    private Settings mSettings;
    private Random mRandom;
    private RandomSceneGenerator mRandomSceneGenerator;
    private INotificationClient mNotificationService;
  }
}
