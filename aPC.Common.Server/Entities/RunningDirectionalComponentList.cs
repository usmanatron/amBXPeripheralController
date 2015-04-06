using aPC.Common;
using aPC.Common.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Common.Server.Entities
{
  public class RunningDirectionalComponentList
  {
    private RunningDirectionalComponent frame;
    private List<RunningDirectionalComponent> components;
    private bool inUpdateMode;

    public eSceneType SceneType { get; private set; }

    public List<DirectionalComponent> LastUpdatedDirectionalComponents { get; private set; }

    public RunningDirectionalComponentList()
    {
      this.components = new List<RunningDirectionalComponent>();
      this.LastUpdatedDirectionalComponents = new List<DirectionalComponent>();
    }

    #region Retrieval

    public RunningDirectionalComponent Get(DirectionalComponent directionalComponent)
    {
      return components.Single(component => component.DirectionalComponent == directionalComponent);
    }

    public RunningDirectionalComponent GetSync()
    {
      return frame;
    }

    #endregion Retrieval

    public void StartUpdate(eSceneType sceneType)
    {
      inUpdateMode = true;
      this.SceneType = sceneType;
      LastUpdatedDirectionalComponents.Clear();
    }

    public void EndUpdate()
    {
      inUpdateMode = false;
    }

    public void Update(amBXScene scene, DirectionalComponent directionalComponent)
    {
      ThrowIfNotInUpdateMode();
      var existingComponent = components.SingleOrDefault(component => component.DirectionalComponent == directionalComponent);
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