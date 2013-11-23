using Common.Entities;

namespace Common.Accessors
{
  public class FrameAccessor
  {
    public Frame AllOff
    {
      get
      {
        var lAccessor = new ComponentAccessor();

        return new Frame
        {
          Lights = (LightComponent)lAccessor.GetComponent(eComponentType.Light, "Off"),
          Fans = (FanComponent)lAccessor.GetComponent(eComponentType.Fan, "Off"),
          Rumble = (RumbleComponent)lAccessor.GetComponent(eComponentType.Rumble, "Off"),
          Length = 1000,
          IsRepeated = false
        };
      }
    }

  }
}
