using aPC.Common;
using aPC.Common.Communication;
using aPC.Common.Defaults;
using aPC.Common.Entities;
using System;
using System.IO;
using System.Linq;
using System.Xml.Serialization;

namespace aPC.Server.Communication
{
  public class NotificationService : INotificationService
  {
    public NotificationService()
      : this(scene => Server.ServerTask.Update(scene))
    {
    }

    // Used for tests
    public NotificationService(Action<amBXScene> xiUpdateScene)
    {
      mAction = xiUpdateScene;
    }

    public void RunCustomScene(string xiSceneXml)
    {
      var lScene = DeserialiseScene(xiSceneXml);
      UpdateScene(lScene);
    }

    public void RunIntegratedScene(string xiSceneName)
    {
      var lAccessor = new SceneAccessor(new DefaultScenes());
      var lScene = lAccessor.GetScene(xiSceneName) ??
                   lAccessor.GetScene("Error_Flash");

      UpdateScene(lScene);
    }

    public string[] GetSupportedIntegratedScenes()
    {
      var lAccessor = new SceneAccessor(new DefaultScenes());

      return lAccessor.GetAllScenes()
        .Select(scene => scene.Key)
        .ToArray();
    }

    private amBXScene DeserialiseScene(string xiSceneXml)
    {
      using (var lReader = new StringReader(xiSceneXml))
      {
        var lSerialiser = new XmlSerializer(typeof(amBXScene));
        return (amBXScene)lSerialiser.Deserialize(lReader);
      }
    }

    protected void UpdateScene(amBXScene xiScene)
    {
      mAction(xiScene);
    }

    private Action<amBXScene> mAction;
  }
}