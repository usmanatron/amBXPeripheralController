using System;
using System.IO;
using System.Xml.Serialization;
using System.Threading;
using aPC.Client.Disco.Generators;
using aPC.Client.Disco.Communication;
using aPC.Common.Communication;
using aPC.Common.Entities;

namespace aPC.Client.Disco
{
  public class DiscoTask
  {
    public DiscoTask(Settings xiSettings,
      IGenerator<amBXScene> xiRandomSceneGenerator, 
      INotificationClient xiNotificationService)
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
      mNotificationService.PushCustomScene(SerialiseScene(xiScene));
    }

    private string SerialiseScene(amBXScene xiScene)
    {
      using (var lWriter = new StringWriter())
      {
        var lSerializer = new XmlSerializer(typeof(amBXScene));
        lSerializer.Serialize(lWriter, xiScene);
        return lWriter.ToString();
      }
    }

    private void WaitForInterval()
    {
      Thread.Sleep(mSettings.PushInterval);
    }
    
    private Settings mSettings;
    private IGenerator<amBXScene> mRandomSceneGenerator;
    private INotificationClient mNotificationService;
  }
}
