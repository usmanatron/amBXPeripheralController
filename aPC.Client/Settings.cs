using aPC.Common.Client;

namespace aPC.Client
{
  public class Settings : ISettings
  {
    public bool IsValid
    {
      get
      {
        return !string.IsNullOrEmpty(SceneData);
      }
    }

    public bool IsIntegratedScene;
    public string SceneData;
  }
}
