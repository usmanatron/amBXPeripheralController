using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace Common.Entities
{

  // The properly planned class setup, which will be Xml serialisable etc.
  [XmlRoot]
  [Serializable]
  public class amBXScene
  {
    [XmlAttribute]
    public int Version;

    // A complicated one.  The idea is that we want to mess with fans without touching the light (for example).
    // Saying you're exclusive means that we kill off everything and just use this.  Otherwise we'll merge in the 
    // changes specified by this.
    [XmlAttribute]
    public bool IsExclusive;

    // How long each frame is.  Too complicated to have per input for now
    [XmlAttribute]
    public int FrameLength;

    [XmlArray("LightFrames")]
    [XmlArrayItem("LightFrame")]
    public List<LightFrame> LightFrames; //cf ColourWheel

    [XmlArray("FanFrames")]
    [XmlArrayItem("FanFrame")]
    public List<FanFrame> FanFrames;

    [XmlArray("RumbleFrames")]
    [XmlArrayItem("RumbleFrame")]
    public List<RumbleFrame> RumbleFrames;


    #region Helper Properties

    public bool LightSpecified
    {
      get
      {
        return LightFrames != null && LightFrames.Any();
      }
    }

    public bool FanSpecified
    {
      get
      {
        return FanFrames != null && FanFrames.Any();
      }
    }

    public bool RumbleSpecified
    {
      get
      {
        return RumbleFrames != null && RumbleFrames.Any();
      }
    }

    public List<LightFrame> RepeatableLightFrames
    {
      get
      {
        return LightFrames.Where(frame => frame.IsRepeated).ToList();
      }
    }

    public List<FanFrame> RepeatableFanFrames
    {
      get
      {
        return FanFrames.Where(frame => frame.IsRepeated).ToList();
      }
    }

    public List<RumbleFrame> RepeatableRumbleFrames
    {
      get
      {
        return RumbleFrames.Where(frame => frame.IsRepeated).ToList();
      }
    }

    #endregion
  }
}
