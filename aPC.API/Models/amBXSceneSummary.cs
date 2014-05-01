using System;
using System.Collections.Generic;
using System.Web;
using aPC.Common.Entities;
using aPC.Common;
using System.Xml.Serialization;

namespace aPC.API.Models
{
  /// <summary>
  /// Represents summary information from a given amBXScene
  /// </summary>
  [XmlRoot]
  [Serializable]
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

    [XmlElement]
    public string SceneName;

    [XmlElement]
    public bool IsEvent;

    [XmlElement]
    public bool IsSynchronised;

    [XmlElement]
    public int SceneLength;
  }
}