using aPC.Common.Entities;

namespace aPC.Common.Accessors
{
  public class FrameAccessor
  {
    public Frame AllOff
    {
      get
      {
        var lAccessor = new SectionAccessor();

        return new Frame
        {
          Lights = (LightSection)lAccessor.GetSection(eSectionType.Light, "Off"),
          Fans = (FanSection)lAccessor.GetSection(eSectionType.Fan, "Off"),
          Rumbles = (RumbleSection)lAccessor.GetSection(eSectionType.Rumble, "Off"),
          Length = 1000,
          IsRepeated = false
        };
      }
    }

  }
}
