using System;
using Common.Defaults;
using Common.Entities;

namespace Common.Accessors
{
  public class ComponentAccessor
  {
    public ComponentBase GetComponent(eComponentType xiFrametype, string xiDescription)
    {
      switch (xiFrametype)
      {
        case eComponentType.Light:
          return GetLightComponent(xiDescription.ToLower());
        case eComponentType.Fan:
          return GetFanComponent(xiDescription.ToLower());
        case eComponentType.Rumble:
          return GetRumbleComponent(xiDescription.ToLower());
        default:
          throw new InvalidOperationException("Unexpected Frame type");
      }
    }

    private LightComponent GetLightComponent(string xiDescription)
    {
      switch (xiDescription)
      {
        case "off":
          return DefaultLightComponents.Off;
        default:
          throw new InvalidOperationException("Unexpected Light frame type");
      }
    }

    private FanComponent GetFanComponent(string xiDescription)
    {
      switch (xiDescription)
      {
        case "off":
          return DefaultFanComponents.Off;
        default:
          throw new InvalidOperationException("Unexpected Fan frame type");
      }
    }

    private RumbleComponent GetRumbleComponent(string xiDescription)
    {
      switch (xiDescription)
      {
        case "off":
          return DefaultRumbleComponents.Off;
        default:
          throw new InvalidOperationException("Unexpected Rumble frame type");
      }
    }

  }
}
