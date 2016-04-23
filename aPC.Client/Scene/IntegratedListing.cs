using aPC.Client.Communication;
using aPC.Common;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Scene
{
  public class IntegratedListing : ISceneListing
  {
    public Dictionary<string, string> Scenes { get; private set; }

    private readonly SceneAccessor sceneAccessor;
    private readonly NotificationClient notificationClient;

    public IntegratedListing(SceneAccessor sceneAccessor, NotificationClient notificationClient)
    {
      this.sceneAccessor = sceneAccessor;
      this.notificationClient = notificationClient;
      LoadScenes();
    }

    public void Reload()
    {
      LoadScenes();
    }

    private void LoadScenes()
    {
      Scenes = new Dictionary<string, string>();

      var scenes = notificationClient.GetAvailableScenes()
         .OrderBy(scene => scene);

      foreach (var scene in scenes)
      {
        Scenes.Add(scene, scene);
      }
    }

    public IEnumerable<string> DropdownListing
    {
      get
      {
        return Scenes.Keys;
      }
    }

    public string GetValue(string key)
    {
      return Scenes[key];
    }

    public string BrowseItemName
    {
      get
      {
        return string.Empty;
      }
    }
  }
}