using aPC.Common;
using aPC.Common.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace aPC.Web.Models
{
  /// <summary>
  /// Represents summary information from a given amBXScene
  /// </summary>
  [DataContract]
  public class amBXSceneSummary
  {
    public amBXSceneSummary(KeyValuePair<string, amBXScene> scene)
    {
      SceneName = scene.Key;
      SceneLength = scene.Value.FrameStatistics.SceneLength;

      var sceneType = scene.Value.SceneType;
      IsEvent = sceneType == eSceneType.Event;
      IsSynchronised = sceneType == eSceneType.Sync || sceneType == eSceneType.Event;
    }

    [DataMember]
    public string SceneName;

    [DataMember]
    public bool IsEvent;

    [DataMember]
    public bool IsSynchronised;

    [DataMember]
    public int SceneLength;
  }
}