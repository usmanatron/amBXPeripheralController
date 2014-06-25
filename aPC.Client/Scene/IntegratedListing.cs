using aPC.Common;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace aPC.Client.Scene
{
  class IntegratedListing : ISceneListing
  {
    public IntegratedListing()
    {
      LoadScenes();
    }

    private void LoadScenes()
    {
      Scenes = new Dictionary<string, string>();
    
      var lScenes = new SceneAccessor()
        .GetAllScenes()
        .Select(scene => scene.Key)
        .OrderBy(scene => scene);

      foreach (var lScene in lScenes)
      {
        Scenes.Add(lScene, lScene);
      }
    }

    public string BrowseItemName 
    {
      get
      {
        return string.Empty;
      }
    }

    public Dictionary<string, string> Scenes { get; private set; }
  }
}
