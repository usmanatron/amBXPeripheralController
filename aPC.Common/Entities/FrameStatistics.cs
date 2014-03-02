using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace aPC.Common.Entities
{
  [Serializable]
  public class FrameStatistics
  {
    /// <remarks>
    ///   For now, just go through all of the frames and process them - forgoe
    ///   any potential performance gains of doing anything smarter
    /// </remarks>
    public FrameStatistics(List<Frame> xiFrames)
    {
      EnabledDirectionalComponents = new List<Tuple<eComponentType, eDirection>>();

      foreach (var lFrame in xiFrames)
      {
        ProcessFrame(lFrame);
      }
    }

    private void ProcessFrame(Frame xiFrame)
    {
      ProcessComponent<Light>(xiFrame.Lights);
      ProcessComponent<Fan>(xiFrame.Fans);
      ProcessComponent<Rumble>(xiFrame.Rumbles);
    }

    /// <remarks>
    ///   qqUMI The following commented out portion is an alternative method which *may*
    ///         be more efficient (as it'll be looking at less directions).  though it isn't as clean.
    ///  var lDirections = xiSection
    ///    .GetType()
    ///    .GetFields()
    ///    .Select(field => field.GetCustomAttribute<DirectionAttribute>())
    ///    .Where(attribute => attribute != null)
    ///    .Select(attribute => attribute.Direction);
    ///  foreach (var lDirection in lDirections)
    ///  {
    ///    AddDirectionalComponent(new T(), lDirection);
    ///  }
    /// </remarks>
    private void ProcessComponent<T>(SectionBase<T> xiSection) where T : IComponent
    {
      foreach (eDirection lDirection in Enum.GetValues(typeof(eDirection)))
      {
        var lComponent = xiSection.GetComponentValueInDirection(lDirection);
        if (lComponent != null)
        {
          AddDirectionalComponent(lComponent.ComponentType(), lDirection);
        }
      }
    }

    private void AddDirectionalComponent(eComponentType xiComponentType, eDirection xiDirection)
    {
      if (!AreEnabledForComponentAndDirection(xiComponentType, xiDirection))
      {
        EnabledDirectionalComponents.Add(new Tuple<eComponentType, eDirection>(xiComponentType, xiDirection));
      }
    }

    public bool AreEnabledForComponent(eComponentType xiComponentType)
    {
      return EnabledDirectionalComponents
        .Any(c => HasComponentType(c, xiComponentType));
    }

    public bool AreEnabledForComponentAndDirection(eComponentType xiComponentType, eDirection xiDirection)
    {
      return EnabledDirectionalComponents
        .Any(c => HasComponentType(c, xiComponentType) &&
                  HasDirection(c, xiDirection));
    }

    private Func<Tuple<eComponentType, eDirection>, eComponentType, bool> HasComponentType = 
      (item, componentType) => item.Item1 == componentType;

    private Func<Tuple<eComponentType, eDirection>, eDirection, bool> HasDirection =
      (item, direction) => item.Item2 == direction;

    public readonly List<Tuple<eComponentType, eDirection>> EnabledDirectionalComponents;
  }
}
