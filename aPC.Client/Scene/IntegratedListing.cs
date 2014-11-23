using aPC.Client.Communication;
using aPC.Common;
using System.Collections.Generic;
using System.Linq;

namespace aPC.Client.Scene
{
  public class IntegratedListing : ISceneListing
  {
    public IntegratedListing(SceneAccessor xiSceneAccessor, NotificationClient xiNotificationClient)
    {
      mAccessor = xiSceneAccessor;
      mNotificationClient = xiNotificationClient;
      LoadScenes();
    }

    public void Reload()
    {
      LoadScenes();
    }

    private void LoadScenes()
    {
      Scenes = new Dictionary<string, string>();

      var lScenes = mNotificationClient.GetSupportedIntegratedScenes()
        .OrderBy(scene => scene);

      foreach (var lScene in lScenes)
      {
        Scenes.Add(lScene, lScene);
      }
    }

    public IEnumerable<string> DropdownListing
    {
      get
      {
        return Scenes.Keys;
      }
    }

    public string GetValue(string xiKey)
    {
      return Scenes[xiKey];
    }

    public string BrowseItemName
    {
      get
      {
        return string.Empty;
      }
    }

    public Dictionary<string, string> Scenes { get; private set; }

    private readonly SceneAccessor mAccessor;
    private readonly NotificationClient mNotificationClient;
  }
}