using aPC.Client.Disco.Generators;
using aPC.Common.Client.Communication;
using aPC.Common.Entities;
using System.Threading;

namespace aPC.Client.Disco
{
  public class DiscoTask
  {
    public DiscoTask(Settings settings, IGenerator<amBXScene> randomSceneGenerator, NotificationClientBase notificationService)
    {
      this.settings = settings;
      this.randomSceneGenerator = randomSceneGenerator;
      this.notificationService = notificationService;
    }

    public void Run()
    {
      while (true)
      {
        var scene = GenerateScene();
        PushScene(scene);
        WaitForInterval();
      }
    }

    private amBXScene GenerateScene()
    {
      return randomSceneGenerator.Generate();
    }

    private void PushScene(amBXScene xiScene)
    {
      notificationService.PushCustomScene(xiScene);
    }

    private void WaitForInterval()
    {
      Thread.Sleep(settings.PushInterval);
    }

    private readonly Settings settings;
    private readonly IGenerator<amBXScene> randomSceneGenerator;
    private readonly NotificationClientBase notificationService;
  }
}