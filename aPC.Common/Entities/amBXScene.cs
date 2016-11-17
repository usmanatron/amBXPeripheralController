using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Xml.Serialization;

namespace aPC.Common.Entities
{
  [XmlRoot]
  [Serializable]
  [DataContract]
  public class amBXScene
  {
    // A complicated one.  The idea is that we want to mess with fans without touching the light (for example).
    // Saying you're exclusive means that we kill off everything and just use this.  Otherwise we'll merge in the
    // changes specified by this.
    // TODO: not yet implemented
    [DataMember]
    [XmlElement]
    public bool IsExclusive;

    [XmlElement]
    public eSceneType SceneType;

    /// <summary>
    /// Used for JSON serialisation - a limitation means that enums
    /// are shown by their numerical representation.  This ensures we see the
    /// name instead.
    /// </summary>
    [XmlIgnore]
    [DataMember(Name = "SceneType")]
    public string PropertyOneString
    {
      get
      {
        return Enum.GetName(typeof(eSceneType), SceneType);
      }
      set
      {
        SceneType = (eSceneType)Enum.Parse(typeof(eSceneType), value);
      }
    }

    [DataMember]
    [XmlArray("Frames")]
    [XmlArrayItem("Frame")]
    public List<Frame> Frames
    {
      get
      {
        return frames;
      }
      set
      {
        frames = value;
        // Clear the statistics
        frameStatistics = null;
      }
    }

    #region Helper Properties

    [XmlIgnore]
    public List<Frame> RepeatableFrames
    {
      get
      {
        return Frames.Where(frame => frame.IsRepeated).ToList();
      }
    }

    [XmlIgnore]
    public bool HasRepeatableFrames
    {
      get
      {
        return RepeatableFrames.Any();
      }
    }

    [XmlIgnore]
    public FrameStatistics FrameStatistics
    {
      get
      {
        return frameStatistics ?? (frameStatistics = new FrameStatistics(Frames));
      }
    }

    #endregion Helper Properties

    [XmlIgnore]
    private List<Frame> frames;

    [XmlIgnore]
    private FrameStatistics frameStatistics;
  }
}