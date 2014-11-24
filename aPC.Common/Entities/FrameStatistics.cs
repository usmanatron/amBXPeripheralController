using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Entities
{
  [Serializable]
  public class FrameStatistics
  {
    /// <remarks>
    ///   For now, just go through all of the frames and process them - forgoe
    ///   any potential performance gains of doing anything smarter
    /// </remarks>
    public FrameStatistics(List<Frame> frames)
    {
      EnabledDirectionalComponents = new List<Tuple<eComponentType, eDirection>>();
      SceneLength = 0;

      foreach (var frame in frames)
      {
        ProcessFrame(frame);
      }
    }

    private void ProcessFrame(Frame frame)
    {
      SceneLength += frame.Length;
      ProcessComponent(frame.Lights);
      ProcessComponent(frame.Fans);
      ProcessComponent(frame.Rumbles);
    }

    /// <remarks>
    ///   TODO: The following commented out portion is an alternative method which *may*
    ///         be more efficient (as it'll be looking at less directions).  Need to ensure
    ///         this area is fully tested before dropping this in and confirming:
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
    private void ProcessComponent(IComponentSection section)
    {
      foreach (eDirection direction in Enum.GetValues(typeof(eDirection)))
      {
        var component = section.GetComponentValueInDirection(direction);
        if (component != null)
        {
          AddDirectionalComponent(component.ComponentType(), direction);
        }
      }
    }

    private void AddDirectionalComponent(eComponentType componentType, eDirection direction)
    {
      if (!AreEnabledForComponentAndDirection(componentType, direction))
      {
        EnabledDirectionalComponents.Add(new Tuple<eComponentType, eDirection>(componentType, direction));
      }
    }

    public bool AreEnabledForComponent(eComponentType componentType)
    {
      return EnabledDirectionalComponents
        .Any(c => HasComponentType(c, componentType));
    }

    public bool AreEnabledForComponentAndDirection(eComponentType componentType, eDirection direction)
    {
      return EnabledDirectionalComponents
        .Any(c => HasComponentType(c, componentType) &&
                  HasDirection(c, direction));
    }

    private Func<Tuple<eComponentType, eDirection>, eComponentType, bool> HasComponentType =
      (item, componentType) => item.Item1 == componentType;

    private Func<Tuple<eComponentType, eDirection>, eDirection, bool> HasDirection =
      (item, direction) => item.Item2 == direction;

    public readonly List<Tuple<eComponentType, eDirection>> EnabledDirectionalComponents;
    public int SceneLength;
  }
}