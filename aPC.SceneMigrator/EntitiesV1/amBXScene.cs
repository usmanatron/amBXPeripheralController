using aPC.Common;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace aPC.SceneMigrator.EntitiesV1
{
  [XmlRoot]
  [Serializable]
  public class amBXScene
  {
    [XmlElement]
    public bool IsExclusive;

    [XmlElement]
    public eSceneType SceneType;

    [XmlArray("Frames")]
    [XmlArrayItem("Frame")]
    public List<Frame> Frames;
  }
}