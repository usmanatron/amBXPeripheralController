using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Common.Entities
{
  [XmlRoot]
  [Serializable]
  public class amBXScene
  {
    // A complicated one.  The idea is that we want to mess with fans without touching the light (for example).
    // Saying you're exclusive means that we kill off everything and just use this.  Otherwise we'll merge in the 
    // changes specified by this.
    // not yet implemented
    [XmlAttribute]
    public bool IsExclusive;

    [XmlArray("Frames")]
    [XmlArrayItem("Frame")]
    public List<Frame> Frames;

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

    //qqUMI try to remove all the frames stuff below
    #region Lights

    [XmlIgnore]
    public bool LightSpecified
    {
      get
      {
        return Frames.Any(frame => frame.Lights != null);
      }
    }

    [XmlIgnore]
    public List<LightComponent> LightFrames
    {
      get
      {
        return Frames.Select(frame => frame.Lights)
                     .ToList();
      }
    }

    [XmlIgnore]
    public List<LightComponent> RepeatableLightFrames
    {
      get
      {
        return Frames.Where(frame => frame.IsRepeated)
                     .Select(frame => frame.Lights)
                     .ToList();
      }
    }

    #endregion

    #region Fans


    [XmlIgnore]
    public bool FanSpecified
    {
      get
      {
        return Frames.Any(frame => frame.Fans != null);
      }
    }

    [XmlIgnore]
    public List<FanComponent> FanFrames
    {
      get
      {
        return Frames.Select(frame => frame.Fans)
                     .ToList();
      }
    }

    [XmlIgnore]
    public List<FanComponent> RepeatableFanFrames
    {
      get
      {
        return Frames.Where(frame => frame.IsRepeated)
                     .Select(frame => frame.Fans)
                     .ToList();
      }
    }

    #endregion

    #region Rumble

    [XmlIgnore]
    public bool RumbleSpecified
    {
      get
      {
        return Frames.Any(frame => frame.Rumble != null);
      }
    }

    [XmlIgnore]
    public List<RumbleComponent> RumbleFrames
    {
      get
      {
        return Frames.Select(frame => frame.Rumble)
                     .ToList();
      }
    }

    [XmlIgnore]
    public List<RumbleComponent> RepeatableRumbleComponents
    {
      get
      {
        return Frames.Where(frame => frame.IsRepeated)
                     .Select(frame => frame.Rumble)
                     .ToList();
      }
    }

    #endregion

    #endregion
  }
}
