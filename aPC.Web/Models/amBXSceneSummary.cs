using System.Collections.Generic;
using System.Runtime.Serialization;
using aPC.Common.Entities;
using aPC.Common;

namespace aPC.Web.Models
{
  /// <summary>
  /// Represents summary information from a given amBXScene
  /// </summary>
  [DataContract]
  public class amBXSceneSummary
  {
    public amBXSceneSummary(KeyValuePair<string, amBXScene> xiScene)
    {
      SceneName = xiScene.Key;
      SceneLength = xiScene.Value.FrameStatistics.SceneLength;
      
      var lSceneType = xiScene.Value.SceneType;
      IsEvent = lSceneType == eSceneType.Event;
      IsSynchronised = lSceneType == eSceneType.Sync || lSceneType == eSceneType.Event;
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