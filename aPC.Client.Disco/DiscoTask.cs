using System.IO;
using System.Xml.Serialization;
using System.Threading;
using aPC.Client.Disco.Generators;
using aPC.Common.Communication;
using aPC.Common.Entities;
using aPC.Common.Client.Communication;

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
