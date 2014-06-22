namespace aPC.Client
{
  public class Settings
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
