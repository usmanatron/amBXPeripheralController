using aPC.Client.Disco.Generators;
using aPC.Common.Client.Communication;
using aPC.Common.Entities;
using System;
using System.Threading;

namespace aPC.Client.Disco
{
  public class DiscoTask
  {
    private readonly Settings settings;
    private readonly IGenerator<amBXScene> randomSceneGenerator;
    private readonly NotificationClientBase notificationService;
    private readonly Random random;

    public DiscoTask(Settings settings, IGenerator<amBXScene> randomSceneGenerator, NotificationClientBase notificationService, Random random)
    {
      this.settings = settings;
      this.randomSceneGenerator = randomSceneGenerator;
      this.notificationService = notificationService;
      this.random = random;
    }

    public void Run()
    {
      while (true)
      {
        var scene = GenerateScene();

        if (random.NextDouble() < settings.ChangeThreshold)
        {
          PushScene(scene);
        }

        WaitForInterval();
      }
    }

    private amBXScene GenerateScene()
    {
      return randomSceneGenerator.Generate();
    }

    private void PushScene(amBXScene scene)
    {
      notificationService.PushScene(scene);
    }

    private void WaitForInterval()
    {
      Thread.Sleep(settings.PushInterval);
    }
  }
}