using System;
using System.Collections.Generic;
using System.Threading;
using aPC.Common.Entities;
using aPC.Client.Disco.Generators;
using aPC.Client.Disco.Communication;

namespace aPC.Client.Disco
{
  class DiscoTask
  {
    public DiscoTask(Settings xiSettings)
    {
      mSettings = xiSettings;
      mRandom = new Random();
      mRandomSceneGenerator = new RandomSceneGenerator(mSettings, mRandom);
      mNotificationService = new NotificationService();
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
      mNotificationService.Push(xiScene);
    }

    private void WaitForInterval()
    {
      Thread.Sleep(mSettings.PushInterval);
    }
    
    private Settings mSettings;
    private Random mRandom;
    private RandomSceneGenerator mRandomSceneGenerator;
    private NotificationService mNotificationService;
  }
}
