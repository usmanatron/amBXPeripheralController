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
    [XmlAttribute]
    public bool IsExclusive;

    [XmlAttribute]
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
        return Enum.GetName(typeof(eSceneType), this.SceneType);
      }
      set
      {
        this.SceneType = (eSceneType)Enum.Parse(typeof(eSceneType), value);
      }
    }  


    [DataMember]
    [XmlArray("Frames")]
    [XmlArrayItem("Frame")]
    public List<Frame> Frames
    {
      get
      {
        return mFrames;
      }
      set
      {
        mFrames = value;
        // Clear the statistics
        mFrameStatistics = null;
      }
    }

    #region Helper Properties

    [XmlIgnore]
    public List<Frame> RepeatableFrames
    {
      get
      {
        return Frames.Where(frame => frame.IsRepeated)
                     .ToList();
      }
    }

    [XmlIgnore]
    public FrameStatistics FrameStatistics
    {
      get
      {
        if (mFrameStatistics == null)
        {
          mFrameStatistics = new FrameStatistics(Frames);
        }
        return mFrameStatistics;
      }
    }

    #endregion

    [XmlIgnore]
    private List<Frame> mFrames;

    [XmlIgnore]
    private FrameStatistics mFrameStatistics;
  }
}
