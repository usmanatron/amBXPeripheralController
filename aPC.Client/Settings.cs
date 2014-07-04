namespace aPC.Client
{
  public class Settings
  {
    public Settings() : this(default(bool), string.Empty)
    { 
    }

    public Settings(bool xiIsIntegratedScene, string xiSceneData)
    {
      IsIntegratedScene = xiIsIntegratedScene;
      SceneData = xiSceneData;
    }

    public void Update(bool xiIsIntegratedScene, string xiSceneData)
    {
      IsIntegratedScene = xiIsIntegratedScene;
      SceneData = xiSceneData;
    }

    public bool IsValid
    {
      get
      {
        return !string.IsNullOrEmpty(SceneData);
      }
    }

    public bool IsIntegratedScene { get; private set; }
    public string SceneData { get; private set; }
  }
}
