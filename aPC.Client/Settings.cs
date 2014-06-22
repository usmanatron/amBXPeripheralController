namespace aPC.Client
{
  public class Settings
  {
    public Settings(bool xiIsIntegratedScene, string xiSceneData)
    {
      IsIntegratedScene = xiIsIntegratedScene;
      SceneData = xiSceneData;
    }

    public readonly bool IsIntegratedScene;
    public readonly string SceneData;
  }
}
