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
    public DiscoTask(Settings xiSettings,
      IGenerator<amBXScene> xiRandomSceneGenerator, 
      NotificationClientBase xiNotificationService)
    {
      mSettings = xiSettings;
      mRandomSceneGenerator = xiRandomSceneGenerator;
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
      return mRandomSceneGenerator.Generate();
    }

    private void PushScene(amBXScene xiScene)
    {
      mNotificationService.PushCustomScene(xiScene);
    }

    private void WaitForInterval()
    {
      Thread.Sleep(mSettings.PushInterval);
    }
    
    private readonly Settings mSettings;
    private readonly IGenerator<amBXScene> mRandomSceneGenerator;
    private readonly NotificationClientBase mNotificationService;
  }
}
