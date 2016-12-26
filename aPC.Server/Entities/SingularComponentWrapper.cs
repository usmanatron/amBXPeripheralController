using System;
using System.Collections.Generic;
using aPC.Common;
using aPC.Common.Entities;

namespace aPC.Server.Entities
{
  public class SingularComponentWrapper : ComponentWrapperBase
  {
    public SingularComponentWrapper(amBXScene scene, AtypicalFirstRunInfiniteTicker ticker) 
      : base(scene, ticker)
    {
    }

    public override eSceneType ComponentType => eSceneType.Composite;

    public override IEnumerable<DirectionalComponent> GetNextComponentsToRun()
    {
      var frame = GetFrame();

      foreach (eComponentType componentType in Enum.GetValues(typeof (eComponentType)))
        foreach (eDirection direction in EnumExtensions.GetCompassDirections())
        {
          var component = frame.GetComponentInDirection(componentType, direction);
          if (component != null)
          {
            yield return component;
          }
        }
    }
  }
}
