using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Server.Entities
{
  public class RunningDirectionalComponentList
  {
    private RunningDirectionalComponent frame;
    private readonly List<RunningDirectionalComponent> components;
    private bool inUpdateMode;

    public eSceneType SceneType { get; private set; }

    public List<DirectionalComponent> LastUpdatedDirectionalComponents { get; }

    public RunningDirectionalComponentList()
    {
      components = new List<RunningDirectionalComponent>();
      LastUpdatedDirectionalComponents = new List<DirectionalComponent>();
    }

    #region Retrieval

    public IEnumerable<RunningDirectionalComponent> Get(eSceneType sceneType)
    {
      switch (sceneType)
      {
        case eSceneType.Sync:
        case eSceneType.Event:
          yield return frame;
          break;
        case eSceneType.Desync:
          foreach (var directionalComponent in LastUpdatedDirectionalComponents)
          {
            yield return components.SingleOrDefault(component => component.DirectionalComponent.Equals(directionalComponent));
          }
          break;
        default:
          throw new ArgumentException($"Unexpected SceneType: {sceneType}");
      }
    }

    #endregion Retrieval

    public void StartUpdate(eSceneType sceneType)
    {
      inUpdateMode = true;
      SceneType = sceneType;
      LastUpdatedDirectionalComponents.Clear();
    }

    public void EndUpdate()
    {
      inUpdateMode = false;
    }

    public void Update(amBXScene scene, DirectionalComponent directionalComponent)
    {
      ThrowIfNotInUpdateMode();
      var existingComponent = components
        .SingleOrDefault(component => component.DirectionalComponent.Equals(directionalComponent));
      if (existingComponent != null)
      {
        components.Remove(existingComponent);
      }

      components.Add(new RunningDirectionalComponent(scene, directionalComponent, new AtypicalFirstRunInfiniteTicker(scene)));
      LastUpdatedDirectionalComponents.Add(directionalComponent);
    }

    /// <remarks>
    ///   The DirectionalComponent for frames is ignored
    /// </remarks>
    public void UpdateSync(amBXScene scene)
    {
      frame = new RunningDirectionalComponent(scene, new DirectionalComponent(eComponentType.Light, eDirection.Everywhere), new AtypicalFirstRunInfiniteTicker(scene));
    }

    public void Clear()
    {
      ThrowIfNotInUpdateMode();
      components.Clear();
    }

    private void ThrowIfNotInUpdateMode()
    {
      if (!inUpdateMode)
      {
        throw new InvalidOperationException("Attempted to update when not in Update Mode.");
      }
    }
  }
}