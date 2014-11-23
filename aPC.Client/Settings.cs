namespace aPC.Client
{
  public class Settings
  {
    public bool IsIntegratedScene { get; private set; }

    public string SceneData { get; private set; }

    public Settings()
      : this(default(bool), string.Empty)
    {
    }

    public Settings(bool isIntegratedScene, string sceneData)
    {
      IsIntegratedScene = isIntegratedScene;
      SceneData = sceneData;
    }

    public void Update(bool isIntegratedScene, string sceneData)
    {
      IsIntegratedScene = isIntegratedScene;
      SceneData = sceneData;
    }

    public bool IsValid
    {
      get
      {
        return !string.IsNullOrEmpty(SceneData);
      }
    }
  }
}