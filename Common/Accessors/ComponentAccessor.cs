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
          return GetLightComponent(xiDescription);
        case eComponentType.Fan:
          return GetFanComponent(xiDescription);
        case eComponentType.Rumble:
          return GetRumbleComponent(xiDescription);
        default:
          throw new InvalidOperationException("Unexpected Frame type");
      }
    }

    private LightComponent GetLightComponent(string xiDescription)
    {
      switch (xiDescription)
      {
        case "AllOff":
          return DefaultLightComponents.Off;
        default:
          throw new InvalidOperationException("Unexpected Light frame type");
      }
    }

    private FanComponent GetFanComponent(string xiDescription)
    {
      throw new NotImplementedException();
    }

    private RumbleComponent GetRumbleComponent(string xiDescription)
    {
      throw new NotImplementedException();
    }

  }
}
