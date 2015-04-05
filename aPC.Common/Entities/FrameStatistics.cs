using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Entities
{
  [Serializable]
  public class FrameStatistics
  {
    private Func<DirectionalComponent, eComponentType, bool> HasComponentType =
      (directionalComponent, componentType) => directionalComponent.ComponentType == componentType;

    private Func<DirectionalComponent, eDirection, bool> HasDirection =
      (directionalComponent, direction) => directionalComponent.Direction == direction;

    public readonly List<DirectionalComponent> EnabledDirectionalComponents;
    public int SceneLength;

    /// <remarks>
    ///   For now, just go through all of the frames and process them - forgoe
    ///   any potential performance gains of doing anything smarter
    /// </remarks>
    public FrameStatistics(List<Frame> frames)
    {
      EnabledDirectionalComponents = new List<DirectionalComponent>();
      SceneLength = 0;

      foreach (var frame in frames)
      {
        ProcessFrame(frame);
      }
    }

    private void ProcessFrame(Frame frame)
    {
      SceneLength += frame.Length;
      ProcessComponent(frame.LightSection);
      ProcessComponent(frame.FanSection);
      ProcessComponent(frame.RumbleSection);
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
        var component = section.GetComponentSectionInDirection(direction);
        if (component != null)
        {
          AddDirectionalComponent(component.ComponentType, direction);
        }
      }
    }

    private void AddDirectionalComponent(eComponentType componentType, eDirection direction)
    {
      if (!AreEnabledForComponentAndDirection(componentType, direction))
      {
        EnabledDirectionalComponents.Add(new DirectionalComponent(componentType, direction));
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
  }
}