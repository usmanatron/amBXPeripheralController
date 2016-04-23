using aPC.Common.Entities;
using System;

namespace aPC.Client.Shared
{
  public class Settings
  {
    public string SceneName { get; private set; }
    public amBXScene Scene { get; private set; }
    private bool Assigned;

    public Settings()
    {
      Assigned = false;
    }

    public void SetScene(amBXScene scene)
    {
      ThrowIfAlreadyAssignedOnce();
      Scene = scene;
      Assigned = true;
    }

    public void SetSceneName(string sceneName)
    {
      ThrowIfAlreadyAssignedOnce();
      SceneName = sceneName;
      Assigned = true;
    }

    private void ThrowIfAlreadyAssignedOnce()
    {
      if (Assigned)
      {
        throw new InvalidOperationException("Attempted to assign to Settings twice.");
      }
    }

    public bool IsValid
    {
      get
      {
        return Assigned = true &&
               (!string.IsNullOrEmpty(SceneName) || Scene != null);
      }
    }
  }
}