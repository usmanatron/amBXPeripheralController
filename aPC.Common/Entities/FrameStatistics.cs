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
      mEnabledDirectionalComponents = new List<Tuple<eComponentType, eDirection>>();

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
    ///         be more efficient (as it'll be looking at less directions
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
    private void ProcessComponent<T>(SectionBase<T> xiSection) where T : IComponent, new()
    {
      foreach (eDirection lDirection in Enum.GetValues(typeof(eDirection)))
      {
        var lComponent = xiSection.GetComponentValueInDirection(lDirection);
        if (lComponent != null)
        {
          AddDirectionalComponent(lComponent, lDirection);
        }
      }
    }

    private void AddDirectionalComponent(IComponent xiComponent, eDirection xiDirection)
    {
      if (!AreEnabledForComponentAndDirection(xiComponent, xiDirection));
      {
        mEnabledDirectionalComponents.Add(new Tuple<eComponentType, eDirection>(xiComponent.ComponentType(), xiDirection));
      }
    }

    public bool AreEnabledForComponent(IComponent xiComponent)
    {
      return mEnabledDirectionalComponents
        .Any(c => HasComponentType(c, xiComponent));
    }

    public bool AreEnabledForComponentAndDirection(IComponent xiComponent, eDirection xiDirection)
    {
      return mEnabledDirectionalComponents
        .Any(c => HasComponentType(c, xiComponent) &&
                  HasDirection(c, xiDirection));
    }

    private Func<Tuple<eComponentType, eDirection>, IComponent, bool> HasComponentType = 
      (item, component) => item.Item1 == component.ComponentType();

    private Func<Tuple<eComponentType, eDirection>, eDirection, bool> HasDirection =
      (item, direction) => item.Item2 == direction;

    private readonly List<Tuple<eComponentType, eDirection>> mEnabledDirectionalComponents;
  }
}
