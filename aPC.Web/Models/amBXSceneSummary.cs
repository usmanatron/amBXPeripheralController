using aPC.Common;
using aPC.Common.Entities;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Linq;

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
      IsEvent = !scene.Value.HasRepeatableFrames;
      IsSynchronised = sceneType == eSceneType.Singular;
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